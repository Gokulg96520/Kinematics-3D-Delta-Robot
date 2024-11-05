using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using TwinCAT.Ads;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Concurrent;

namespace _3D_Delta_Kinematics_VS
{
    public partial class MainForm : Form
    {
        // TwinCAT PLC Thread 
        private Thread plcThread; // The thread handling PLC communication      
        private CancellationTokenSource ctsPlcThread; // Used to signal thread shutdown

        //TcAds  
        private TcAdsClient tcClient; //ADS Client 
        private PLCStructure.InputStructure PLCToUIStructure; // PLC to UI Structure 
        private PLCStructure.OutputStructure UIToPLCStructure; // UI to PLC Structure
        private int hPLCToUIStructure; //hReference for PLC to UI Structure 
        private int hUIToPLCStructure; //hReference for UI to PLC Structure 
        private System.Timers.Timer plcTimer; // Timer to use in PLC Thread 

        //Post Data to UI Thread
        private SynchronizationContext syncContext;

        //Put Data to PLC Thread 
        //Producer Consumer concept UI Thread is Producer & PLC Thread is Consumer
        private BlockingCollection<PLCStructure.OutputStructure> plcQueue = new BlockingCollection<PLCStructure.OutputStructure>(10);
        private PLCStructure.OutputStructure eventDataA; //For Capturing Data as Events happend Fast 
        private PLCStructure.OutputStructure eventDataB; //For Capturing Data as Events happend Fast 

        //GL Control Data 
        private float _zoom; // Initial zoom factor
        private float _rotationX; // Rotation around X-axis
        private float _rotationY; // Rotation around Y-axis
        private float _moveXDirection; // Rotation around X-axis
        private float _moveYDirection; // Rotation around Y-axis
        private bool _isDragging = false; // For mouse dragging
        private Point _lastMousePosition; // Last mouse position for rotation

        //Inital Position of Delta Robot
        public vec3 MovePlatePos = new vec3(0, 0, -376.0f);

        //Form Closing Flag
        public bool formClosingFlag;

        public MainForm()
        {
            InitializeComponent();
            InitializeFormData();
            InitializeGLComponent();
        }

        private void InitializeFormData()
        {
            //UI Data 
            tbAMSNetID.Text = "192.168.1.19.1.1";
            tbJogSpeed.Text = "50";
            tbNCIOvrridePer.Text = "100";

            //Initialize of Post Data from PLC Thread to UI Thread
            syncContext = SynchronizationContext.Current;

            //Initialize of Put Data from UI Thread to PLC using Blocking Collection
            eventDataA = eventDataB = new PLCStructure.OutputStructure();
            eventDataA.JogSpeed = eventDataB.JogSpeed = 50.0f;
            eventDataA.NCIOverRidePer = eventDataB.NCIOverRidePer = 50.0f;
            plcQueue = new BlockingCollection<PLCStructure.OutputStructure>(new ConcurrentQueue<PLCStructure.OutputStructure>(), 10); // Set max capacity

            //Form Closing 
            formClosingFlag = false;
        }

        #region TwinCAT ADS Communication

        // Event Hanlder for Ads Connect
        private void btnConnect_Click(object sender, EventArgs e)
        {
            startPLCThread();
        }

        // Async Event Hanlder for Ads Disconnect
        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            bool result = await stopPLCThread();
            if (!result)
            {
                tbError.Text = "TwinCAT PLC is already disconnected";
            }
        }

        //Method Start PLC Thread 
        private void startPLCThread()
        {
            //Prevent multiple starts
            if (plcThread == null || !plcThread.IsAlive)
            {
                ctsPlcThread = new CancellationTokenSource(); // Create a new cancellation token
                plcThread = new Thread(() => PLCCommunication(tbAMSNetID.Text, ctsPlcThread.Token));//Create New Thread Object
                plcThread.IsBackground = true; //Make PLC Thread as Background Thread
                plcThread.Start(); //Start PLC Thread
            }
            else
            {
                tbError.Text = "TwinCAT PLC is already connected";
            }

        }

        //PLC Thread Method Runs PLC Communication in Seperate Thread
        private void PLCCommunication(string AMSNetID, CancellationToken Token)
        {

            //Create PLCStructure Object
            PLCToUIStructure = new PLCStructure.InputStructure();
            UIToPLCStructure = new PLCStructure.OutputStructure();

            //Initalize UIToPLC Data 
            UIToPLCStructure.JogSpeed = 50.0f;
            UIToPLCStructure.NCIOverRidePer = 100.0f;

            //TcClient  
            tcClient = new TcAdsClient();

            //Sequence 1 Connecting to tcClient
            try
            {
                tcClient.Connect(AMSNetID, 851);
            }
            catch (Exception err)
            {
                updateConnectButtonColor(Color.Red);
                updateErrorTextBox(err.Message);
                onPLCClientError(err);
                return;
            }

            if (tcClient.IsConnected == true)
            {
                updateConnectButtonColor(Color.GreenYellow);
                updateErrorTextBox("Connected to Controller");

                //Sequence 2 After Connecting Create a Handle
                try
                {
                    hPLCToUIStructure = tcClient.CreateVariableHandle("UIData.stPLC_TO_UI");
                    hUIToPLCStructure = tcClient.CreateVariableHandle("UIData.stUI_TO_PLC");
                }
                catch (Exception err)
                {
                    updateErrorTextBox(err.Message);
                    onPLCClientError(err);
                    return;
                }

                //Sequence 3 Set Enable Monitoring to True
                UIToPLCStructure.EnableMonitoring = true;
                try
                {
                    tcClient.WriteAny(hUIToPLCStructure, UIToPLCStructure);
                }
                catch (Exception err)
                {
                    updateErrorTextBox(err.Message);
                    onPLCClientError(err);
                    return;
                }

                //Sequence 4 Start Timer
                startPLCTimer();

                //Wait for Cancellation request 
                try
                {
                    Token.WaitHandle.WaitOne();// Block here until cancellation is requested
                }
                finally
                {
                    DisconnectTcAds();
                    //Console.WriteLine("PLC loop exiting...");
                }
            }
            else if (tcClient.IsConnected == false)
            {
                updateConnectButtonColor(Color.Red);
                updateErrorTextBox("Controller Not Connected");
            }
        }

        //PLC Thread Method Start PLC Timer 
        private void startPLCTimer()
        {
            plcTimer = new System.Timers.Timer(200);
            plcTimer.Elapsed += OnPLCTimerElapsed;
            plcTimer.AutoReset = true;
            plcTimer.Start();
        }

        //PLC Thread Method Stop PLC Timer 
        private void stopPLCTimer()
        {
            if (plcTimer != null)
            {
                plcTimer.Stop();
                plcTimer.Dispose();
            }
        }

        //PLC Thread Method Disconnect TcAds & Reset Communication Data to Default Value
        public void DisconnectTcAds()
        {
            if (tcClient != null && tcClient.IsConnected)
            {
                //Stop PLC Timer
                stopPLCTimer();

                //Reset Communication Data to Default Value
                UIToPLCStructure.EnableMonitoring = false;
                try
                {
                    tcClient.WriteAny(hUIToPLCStructure, UIToPLCStructure);
                }
                catch (Exception err)
                {
                    updateErrorTextBox(err.Message);
                    onPLCClientError(err);
                    return;
                }

                try
                {
                    tcClient.Dispose();

                    if (tcClient.IsConnected == false)
                    {
                        resetColor();
                        updateErrorTextBox("Controller Disconneted");
                    }
                }
                catch (Exception err)
                {
                    updateConnectButtonColor(Color.Red);
                    updateErrorTextBox(err.Message);
                    onPLCClientError(err);
                    return;
                }
            }
        }

        //PLC Thread Timer Elapsed Event for Cyclic Communication 
        private void OnPLCTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Read Data from PLC
            try
            {
                //Read from PLC
                PLCToUIStructure = (PLCStructure.InputStructure)tcClient.ReadAny(hPLCToUIStructure, typeof(PLCStructure.InputStructure));
                //Post Data to UI Thread
                syncContext.Post(UpdateUI, PLCToUIStructure);
            }
            catch (Exception err)
            {
                //btnConnect.BackColor = Color.Red;
                updateErrorTextBox(err.Message);
                onPLCClientError(err);
                return;
            }

            //Write Data to PLC
            try
            {
                //Life Bit Toggle
                if (UIToPLCStructure.LifeBit == false)
                {
                    UIToPLCStructure.LifeBit = true;
                }
                else
                {
                    UIToPLCStructure.LifeBit = false;
                }

                //Get Data from UI Thread
                if (!plcQueue.IsCompleted)
                {
                    if (plcQueue.TryTake(out PLCStructure.OutputStructure data))
                    {
                        UIToPLCStructure.EnableAllAxis = data.EnableAllAxis;
                        UIToPLCStructure.ResetAllAxis = data.ResetAllAxis;
                        UIToPLCStructure.ConfigKinematicGroup = data.ConfigKinematicGroup;
                        UIToPLCStructure.ResetKinematicGroup = data.ResetKinematicGroup;
                        UIToPLCStructure.XJogPositive = data.XJogPositive;
                        UIToPLCStructure.YJogPositive = data.YJogPositive;
                        UIToPLCStructure.ZJogPositive = data.ZJogPositive;
                        UIToPLCStructure.XJogNegative = data.XJogNegative;
                        UIToPLCStructure.YJogNegative = data.YJogNegative;
                        UIToPLCStructure.ZJogNegative = data.ZJogNegative;
                        UIToPLCStructure.JogSpeed = data.JogSpeed;
                        UIToPLCStructure.NCIAxisGroup = data.NCIAxisGroup;
                        UIToPLCStructure.NCIAxisUnGroup = data.NCIAxisUnGroup;
                        UIToPLCStructure.NCIInteperatorReset = data.NCIInteperatorReset;
                        UIToPLCStructure.RunPartProgram = data.RunPartProgram;
                        UIToPLCStructure.StopPartProgram = data.StopPartProgram;
                        UIToPLCStructure.NCIOverRidePer = data.NCIOverRidePer;
                        UIToPLCStructure.PartProgramName = data.PartProgramName;
                    }
                }
                //Write Data to PLC
                tcClient.WriteAny(hUIToPLCStructure, UIToPLCStructure);

            }
            catch (Exception err)
            {
                updateErrorTextBox(err.Message);
                onPLCClientError(err);
                return;
            }

        }

        //Method onPLCError
        private async void onPLCError()
        {
            await stopPLCThread();
        }

        //Task Method Stop PLC Thread
        private async Task<bool> stopPLCThread()
        {
            // Request cancellation
            if (plcThread != null && plcThread.IsAlive)
            {
                ctsPlcThread.Cancel(); // Signal the thread to stop
                await Task.Run(() => plcThread.Join());
                return true;
            }
            else
            {
                return false;
            }
        }

        //PLC Thread tcClientError Method
        private void onPLCClientError(Exception err)
        {
            Invoke(new Action(onPLCError));
        }

        //PLC Thread Method Invoke UI Thread Show Message Box 
        private void updateErrorTextBox(string message)
        {
            if (InvokeRequired)
            {
                // If we are on a different thread, use Invoke to call the method on the UI thread
                if (!formClosingFlag)
                {
                    Invoke(new Action<string>(updateErrorTextBox), message);
                }
            }
            else
            {
                tbError.Text = message;
            }
        }

        private void updateConnectButtonColor(Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateConnectButtonColor(color)));
            }
            else
            {
                btnConnect.BackColor = color;
            }
        }

        //When Disconnected Reset Colors of UI
        private void resetColor()
        {
            if (InvokeRequired)
            {
                // If we are on a different thread, use Invoke to call the method on the UI thread
                if (!formClosingFlag)
                {
                    Invoke(new Action(resetColor));
                }

            }
            else
            {
                // We are on the UI thread, update the color
                btnConnect.BackColor = SystemColors.Control;
                btnEnableAxis.BackColor = SystemColors.Control;
                btnConfKinGroup.BackColor = SystemColors.Control;
                btnNCIAxisGrp.BackColor = SystemColors.Control;
            }
        }

        #endregion

        #region UI & Render Update 

        private void UpdateUI(object UIdata)
        {
            var PLCToUIStructure = UIdata as PLCStructure.InputStructure;
            if (PLCToUIStructure == null)
            {
                return;
            }

            //MCS & ACS Position
            tbXCord.Text = PLCToUIStructure.X_MCSPos.ToString("F3");
            tbYCord.Text = PLCToUIStructure.Y_MCSPos.ToString("F3");
            tbZCord.Text = PLCToUIStructure.Z_MCSPos.ToString("F3");
            tbM1Cord.Text = PLCToUIStructure.M1_ACSPos.ToString("F3");
            tbM2Cord.Text = PLCToUIStructure.M2_ACSPos.ToString("F3");
            tbM3Cord.Text = PLCToUIStructure.M3_ACSPos.ToString("F3");

            //Axis Enable Status
            if (PLCToUIStructure.AllAxisEnabled == true && PLCToUIStructure.AxisError == false)
            {
                btnEnableAxis.BackColor = Color.GreenYellow;
            }
            else if (PLCToUIStructure.AxisError == true)
            {
                btnEnableAxis.BackColor = Color.Red;
            }
            else
            {
                btnEnableAxis.BackColor = Color.Yellow;
            }

            //Kinematic Group Status
            if (PLCToUIStructure.KinematicGroupingReady == true)
            {
                btnConfKinGroup.BackColor = Color.GreenYellow;

            }
            else if (PLCToUIStructure.KinematicGroupError == true)
            {
                btnConfKinGroup.BackColor = Color.Red;
            }
            else
            {
                btnConfKinGroup.BackColor = Color.Yellow;
            }

            //NCI Status
            if (PLCToUIStructure.NCIAxisGrouped == true && PLCToUIStructure.NCIAXisError == false)
            {
                btnNCIAxisGrp.BackColor = Color.GreenYellow;
            }
            else if (PLCToUIStructure.NCIAXisError == true)
            {
                btnNCIAxisGrp.BackColor = Color.Red;
            }
            else
            {
                btnNCIAxisGrp.BackColor = Color.Yellow;
            }

            //NCI Inteperator Status
            switch (PLCToUIStructure.NCIInterpreterState)
            {
                case 1:
                    tbIntrpState.Text = "Idle";
                    break;
                case 2:
                    tbIntrpState.Text = "Ready";
                    break;
                case 5:
                    tbIntrpState.Text = "Running";
                    break;
                default:
                    tbIntrpState.Text = "UnKnown";
                    break;
            }

            //Part Program Line
            tbPartPrgLine1.Text = PLCToUIStructure.PartProgramLine1;
            tbPartPrgLine2.Text = PLCToUIStructure.PartProgramLine2;
            tbPartPrgLine3.Text = PLCToUIStructure.PartProgramLine3;

            //3D Delta Robot XYZ Update
            MovePlatePos.x = PLCToUIStructure.X_MCSPos;
            MovePlatePos.y = PLCToUIStructure.Y_MCSPos;
            MovePlatePos.z = PLCToUIStructure.Z_MCSPos;

            //Redraw Render 
            glControl.Invalidate();
        }

        #endregion

        #region UI Event

        #region Axis Enable & reset

        //Enable Axis
        private void btnEnableAxis_Click(object sender, EventArgs e)
        {
            if (eventDataA.EnableAllAxis == false)
            {
                eventDataA.EnableAllAxis = true;
            }
            else
            {
                eventDataA.EnableAllAxis = false;
            }
            addDataToQueue(eventDataA);
        }

        //Reset Axis
        private void btnResetAxis_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.ResetAllAxis = true;
            addDataToQueue(eventDataA);
        }

        private void btnResetAxis_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.ResetAllAxis = false;
            addDataToQueue(eventDataB);
        }

        #endregion

        #region Kinematics Group & reset

        //Configure Kinematics Group
        private void btnConfKinGroup_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.ConfigKinematicGroup = true;
            addDataToQueue(eventDataA);
        }

        private void btnConfKinGroup_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.ConfigKinematicGroup = false;
            addDataToQueue(eventDataB);
        }

        //Reset Kinematics Group
        private void btnResetKinGroup_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.ResetKinematicGroup = true;
            addDataToQueue(eventDataA);
        }
        private void btnResetKinGroup_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.ResetKinematicGroup = false;
            addDataToQueue(eventDataB);
        }
        #endregion

        #region NCI Group & Reset
        private void btnNCIAxisGrp_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.NCIAxisGroup = true;
            addDataToQueue(eventDataA);
        }

        private void btnNCIAxisGrp_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.NCIAxisGroup = false;
            addDataToQueue(eventDataB);
        }

        private void btnNCIAxisUnGrp_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.NCIAxisUnGroup = true;
            addDataToQueue(eventDataA);
        }

        private void btnNCIAxisUnGrp_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.NCIAxisUnGroup = false;
            addDataToQueue(eventDataB);
        }

        private void btnNCIIntrReset_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.NCIInteperatorReset = true;
            addDataToQueue(eventDataA);
        }

        private void btnNCIIntrReset_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.NCIInteperatorReset = false;
            addDataToQueue(eventDataB);
        }
        #endregion

        #region Tab Control Events

        #region Jog Tab 

        #region XYZ Jog Mouse Down & Up Event Handler

        private void btnZPos_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.ZJogPositive = true;
            addDataToQueue(eventDataA);
        }

        private void btnZPos_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.ZJogPositive = false;
            addDataToQueue(eventDataB);
        }

        private void btnZNeg_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.ZJogNegative = true;
            addDataToQueue(eventDataA);
        }

        private void btnZNeg_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.ZJogNegative = false;
            addDataToQueue(eventDataB);
        }

        private void btnYNeg_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.YJogNegative = true;
            addDataToQueue(eventDataA);
        }

        private void btnYNeg_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.YJogNegative = false;
            addDataToQueue(eventDataB);
        }

        private void btnYPos_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.YJogPositive = true;
            addDataToQueue(eventDataA);
        }

        private void btnYPos_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.YJogPositive = false;
            addDataToQueue(eventDataB);
        }

        private void btnXNeg_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.XJogNegative = true;
            addDataToQueue(eventDataA);
        }

        private void btnXNeg_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.XJogNegative = false;
            addDataToQueue(eventDataB);
        }

        private void btnXPos_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.XJogPositive = true;
            addDataToQueue(eventDataA);
        }

        private void btnXPos_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.XJogPositive = false;
            addDataToQueue(eventDataB);
        }
        #endregion

        #region Jog Speed

        private void tbJogSpeed_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                float.TryParse(tbJogSpeed.Text, out float speed);
                if ((speed > 1 && speed <= 200))
                {
                    eventDataA.JogSpeed = speed;
                }
                else
                {
                    eventDataA.JogSpeed = 50.0f;
                    tbJogSpeed.Text = "50.0";
                }
                addDataToQueue(eventDataA);

                // Set focus to some other Control
                btnZPos.Focus();

                // Prevent the ding sound on Enter key press
                e.Handled = true;

            }
        }

        #endregion

        #endregion

        #region NCI Tab

        #region Override Percentage
        private void tbNCIOvrridePer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                float.TryParse(tbNCIOvrridePer.Text, out float overridePer);
                if ((overridePer > 1 && overridePer <= 100))
                {
                    eventDataA.NCIOverRidePer = overridePer;
                }
                else
                {
                    eventDataA.NCIOverRidePer = 100.0f;
                    tbNCIOvrridePer.Text = "100.0";
                }

                addDataToQueue(eventDataA);

                // Set focus to some other Control
                btnFileExp.Focus();

                // Prevent the ding sound on Enter key press
                e.Handled = true;
            }
        }

        #endregion

        #region File Selection

        private void btnFileExp_Click(object sender, EventArgs e)
        {
            string selectedFilePath = OpenFileAndReturnFileName();

            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                tbNCProgramName.Text = selectedFilePath;
                eventDataA.PartProgramName = selectedFilePath;
            }
            else
            {
                MessageBox.Show("No file was selected.", "Selection Canceled");
            }
            addDataToQueue(eventDataA);
        }

        // Method to open File Explorer and return the selected file path
        private string OpenFileAndReturnFileName()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\TwinCAT\\Mc\\Nci"; // Default starting directory
                openFileDialog.Filter = "NC Files (*.nc)|*.nc|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                // Show the dialog and return the selected file path if one is chosen
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return Path.GetFileName(openFileDialog.FileName); // Return the selected file path
                }
            }

            return string.Empty; // Return empty string if no file was selected
        }

        #endregion

        #region NCI start & stop

        private void btnStartPartprg_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.RunPartProgram = true;
            addDataToQueue(eventDataA);
        }

        private void btnStartPartprg_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.RunPartProgram = false;
            addDataToQueue(eventDataB);
        }

        private void btnStopPartprg_MouseDown(object sender, MouseEventArgs e)
        {
            eventDataA.StopPartProgram = true;
            addDataToQueue(eventDataA);
        }

        private void btnStopPartprg_MouseUp(object sender, MouseEventArgs e)
        {
            eventDataB.StopPartProgram = false;
            addDataToQueue(eventDataB);
        }

        #endregion

        #endregion

        #endregion

        private void addDataToQueue(PLCStructure.OutputStructure data)
        {
            if (!plcQueue.TryAdd(data))
            {
                tbError.Text = "Adding Data to Queue Failed";
            }
        }
        #endregion

        #region OpenGL GL Control & Render 

        #region GLControl Initialize, Load, Paint & Resize

        // Initalize GL Components
        private void InitializeGLComponent()
        {
            //Initial Camera Position
            _zoom = 30.0f; 
            _rotationX = 30.0f; 
            _rotationY = 50.0f;
            _moveXDirection = -15.0f;
            _moveYDirection = -5.0f;

            glControl.Load += GLControl_Load; // Subscribe to the Load event
            glControl.Paint += GLControl_Paint; // Subscribe to the Paint event
            glControl.Resize += GLControl_Resize; // Subscribe to the Resize event
            glControl.MouseWheel += GlControl_MouseWheel; // Subscribe to MouseWheel event
            glControl.MouseDown += GlControl_MouseDown; // Handle mouse down event
            glControl.MouseMove += GlControl_MouseMove; // Handle mouse move event
            glControl.MouseUp += GlControl_MouseUp; // Handle mouse up event

        }

        // Event handler for when the GLControl is loaded
        private void GLControl_Load(object sender, EventArgs e)
        {

            // Enable blending for transparency
            GL.Enable(EnableCap.DepthTest);

            // Set the clear color to black
            GL.ClearColor(Color.FromArgb(0, 0, 0, 0));

            //
            GL.Enable(EnableCap.CullFace);
        }

        // Event handler for rendering 
        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            // Clear the color and depth buffers
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Set up the projection matrix
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(-1.0, 1.0, -1.0, 1.0, 1.0, 100.0); // Adjust near and far planes
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Zoom: Move back along the Z-axis & also in x,y position
            GL.Translate(_moveXDirection, _moveYDirection, -_zoom);

            // Apply rotation transformations for the entire scene
            GL.Rotate(_rotationX, 1.0, 0.0, 0.0);
            GL.Rotate(_rotationY, 0.0, 1.0, 0.0);

            // Add your rendering code here
            Render();

            // Swap buffers to display the rendered line
            glControl.SwapBuffers();

        }

        // Event handler when GLControl is Resized 
        private void GLControl_Resize(object sender, EventArgs e)
        {
            if (glControl.Width > 0 && glControl.Height > 0)
            {

                GL.Viewport(0, 0, glControl.Width, glControl.Height); // Set the viewport to match the control dimensions
                GL.MatrixMode(MatrixMode.Projection); // Switch to projection matrix mode
                GL.LoadIdentity(); // Reset the projection matrix to the identity matrix

                // Recalculate the projection matrix to maintain aspect ratio
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                float aspectRatio = (float)glControl.Width / (float)glControl.Height;
                GL.Frustum(-aspectRatio, aspectRatio, -1.0, 1.0, 1.0, 100.0);

            }
        }

        #endregion

        #region GLControl Mouse Events

        // Event handler for mouse wheel moves events
        private void GlControl_MouseWheel(object sender, MouseEventArgs e) 
        {
            // Adjust the camera zoom level based on the scroll direction
            _zoom += e.Delta > 0 ? -1f : 1f; ; // Zoom in/out based on the scroll wheel delta
            _zoom = Math.Max(1.0f, _zoom); // Prevent zooming too close
            glControl.Invalidate(); // Request a redraw of the GLControl
        }

        // Event handler for mouse down events
        private void GlControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true; // Start dragging
                _lastMousePosition = e.Location; // Store the last mouse position
            }
        }

        // Event handler for mouse move events
        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the difference in mouse position
                int deltaX = e.Location.X - _lastMousePosition.X;
                int deltaY = e.Location.Y - _lastMousePosition.Y;

                // Update rotation based on mouse movement
                _rotationY += deltaX * 0.5f; // Rotate around Y-axis
                _rotationX -= deltaY * 0.5f; // Rotate around X-axis

                // Clamp the X rotation to prevent flipping
                _rotationX = MathHelper.Clamp(_rotationX, -90.0f, 90.0f);

                _lastMousePosition = e.Location; // Update last mouse position
                glControl.Invalidate(); // Redraw the control
            }
        }

        // Event handler for mouse up events
        private void GlControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false; // Stop dragging
            }
        }

        // Event handler for Move Left View events
        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            _moveXDirection -= 2.0f;
            glControl.Invalidate();
        }

        // Event handler for Move Right View events
        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            _moveXDirection += 2.0f;
            glControl.Invalidate();
        }

        // Event handler for Move Up View events
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            _moveYDirection += 2.0f;
            glControl.Invalidate();
        }

        // Event handler for Move Down View events
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            _moveYDirection -= 2.0f;
            glControl.Invalidate();
        }

        // Event handler for Rest View events
        private void btnReset_Click(object sender, EventArgs e)
        {
            _moveXDirection = -15.0f;
            _moveYDirection = -5.0f;
            _zoom = 30.0f;
            _rotationX = 30.0f;
            _rotationY = 50.0f;
            glControl.Invalidate();
        }

        #endregion

        #region User Defined Render Code

        // Render PipeLine
        private void Render()
        {
            DrawGrid();

            DrawCoordinateAxes();

            Draw3DDeltaRobot();

        }

        // Method to draw XYZ axis
        private void DrawCoordinateAxes()
        {
            CoordinateDrawer CoDrw = new CoordinateDrawer();
            CoDrw.DrawCoordinateAxes();
        }

        // Method to Draw Grid
        private void DrawGrid()
        {
            GL.Color3(Color.Gray);
            GL.Begin(PrimitiveType.Lines);

            int size = 25; // Grid size

            for (int i = 0; i <= size; i=i+3)
            {
                // Lines parallel to X-axis
                GL.Vertex3(i, 0, 0);
                GL.Vertex3(i, 0, size);

                // Lines parallel to Z-axis
                GL.Vertex3(0, 0, i);
                GL.Vertex3(size, 0, i);
            }

            GL.End();
        }

        // Draw 3D Delta Robot 
        private void Draw3DDeltaRobot()
        {
            //Move Robot Position in x,y,z
            GL.Translate(10, 15, 10);

            //Delta Robot Draw
            Delta_3_Robot D3R = new Delta_3_Robot(new vec3(MovePlatePos.x, MovePlatePos.y, MovePlatePos.z));
            D3R.DrawDelta3Robot();
        }





        #endregion

        #endregion

        #region MainForm Closing Event

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Show confirmation dialog
            if (plcThread != null && plcThread.IsAlive)
            {
                var result = MessageBox.Show("Are you sure you want to exit TcAds Communication will be Disconnected",
                             "Confirm Exit",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    formClosingFlag = true;
                    //Stoping PLC Thread
                    await stopPLCThread();
                    //Console.WriteLine("PLC thread stopped");
                }
                else
                {
                    // If user cancels, prevent the form from closing
                    e.Cancel = true;  // Cancel closing
                    return;
                }

            }

        }

        #endregion

    }
}
