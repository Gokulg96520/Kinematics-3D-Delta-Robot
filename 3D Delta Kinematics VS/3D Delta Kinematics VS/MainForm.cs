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
using System.Runtime.InteropServices;

namespace _3D_Delta_Kinematics_VS
{
    public partial class MainForm : Form
    {

        //GL Control Data 
        private float _zoom; // Initial zoom factor
        private float _rotationX ; // Rotation around X-axis
        private float _rotationY ; // Rotation around Y-axis
        private float _moveXDirection; // Rotation around X-axis
        private float _moveYDirection; // Rotation around Y-axis
        private bool _isDragging = false; // For mouse dragging
        private Point _lastMousePosition; // Last mouse position for rotation

        //Inital Position of Delta Robot
        public vec3 MovePlatePos = new vec3(0, 0, -376.0f);

        //TcAds
        private TcAdsClient tcClient;
        private Timer timerCtrl;
        private PLCStructure.InputStructure PLCToUIStructure;
        private PLCStructure.OutputStructure UIToPLCStructure;
        private int hStructure;

        public MainForm()
        {
            InitializeComponent();
            InitializeGLComponent();
            InitializeTcAds();
        }

        #region TwinCAT ADS Communication

        // Initalize Ads
        private void InitializeTcAds()
        {
            tbAMSNetID.Text = "192.168.1.19.1.1";
            PLCToUIStructure = new PLCStructure.InputStructure();
            UIToPLCStructure = new PLCStructure.OutputStructure();
        }

        // Event Hanlder for Ads Connect
        private void btnConnect_Click(object sender, EventArgs e)
        {
            tcClient = new TcAdsClient();
            try
            {
                tcClient.Connect(tbAMSNetID.Text, 851);
            }
            catch (Exception err)
            {
                btnConnect.BackColor = Color.Red;
                MessageBox.Show(err.Message);
            }
            if (tcClient.IsConnected == true)
            {
                btnConnect.BackColor = Color.GreenYellow;
                MessageBox.Show("Connected to Controller");
                timerCtrl = new Timer();
                timerCtrl.Interval = 200;
                timerCtrl.Tick += OnCtrlTimerEvent;
                timerCtrl.Start();
            }
            else if (tcClient.IsConnected == false)
            {
                btnConnect.BackColor = Color.Red;
                MessageBox.Show("Controller 1 Not Connected");
            }
        }

        // Event Hanlder for 500ms Timer
        private void OnCtrlTimerEvent(object sender, EventArgs e)
        {

            try
            {
                hStructure = tcClient.CreateVariableHandle("UIData.stPLC_TO_UI");
                PLCToUIStructure = (PLCStructure.InputStructure)tcClient.ReadAny(hStructure, typeof(PLCStructure.InputStructure));

                hStructure = tcClient.CreateVariableHandle("UIData.stUI_TO_PLC");
                tcClient.WriteAny(hStructure, UIToPLCStructure);

                UpdateUI();

            }
            catch (Exception err)
            {
                if (timerCtrl != null)
                {
                    timerCtrl.Stop();
                    timerCtrl.Dispose();
                }
                btnConnect.BackColor = Color.Red;
                MessageBox.Show(err.Message);
            }


        }

        // Event Hanlder for Ads Connect
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (timerCtrl != null)
            {
                timerCtrl.Stop();
                timerCtrl.Dispose();
            }

            try
            {
                tcClient.Dispose();
                if (tcClient.IsConnected == false)
                {
                    btnConnect.BackColor = SystemColors.Control;
                    MessageBox.Show("Controller Disconneted");
                }
            }
            catch (Exception err)
            {
                btnConnect.BackColor = Color.Red;
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        #region UI & Render Update 

        private void UpdateUI()
        {
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
           

            //3D Delta Robot XYZ Update
            MovePlatePos.x = PLCToUIStructure.X_MCSPos;
            MovePlatePos.y = PLCToUIStructure.Y_MCSPos;
            MovePlatePos.z = PLCToUIStructure.Z_MCSPos;
            
            //Redraw Render 
            glControl.Invalidate();
        }

        #endregion

        #region UI Event

        #region XYZ Jog Mouse Down & Up Event Handler

        private void btnZPos_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.ZJogPositive = true;
        }

        private void btnZPos_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.ZJogPositive = false;
        }

        private void btnZNeg_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.ZJogNegative = true;
        }

        private void btnZNeg_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.ZJogNegative = false;
        }

        private void btnYNeg_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.YJogNegative = true;
        }

        private void btnYNeg_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.YJogNegative = false;
        }

        private void btnYPos_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.YJogPositive = true;
        }

        private void btnYPos_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.YJogPositive = false;
        }

        private void btnXNeg_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.XJogNegative = true;
        }

        private void btnXNeg_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.XJogNegative = false;
        }

        private void btnXPos_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.XJogPositive = true;
        }

        private void btnXPos_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.XJogPositive = false;
        }
        #endregion

        #region Axis Enable & reset

        //Enable Axis
        private void btnEnableAxis_Click(object sender, EventArgs e)
        {
            if (UIToPLCStructure.EnableAllAxis == false)
            {
                UIToPLCStructure.EnableAllAxis = true;
            }
            else
            {
                UIToPLCStructure.EnableAllAxis = false;
            }
        }

        //Reset Axis
        private void btnResetAxis_MouseDown(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.ResetAllAxis = true;
        }

        private void btnResetAxis_MouseUp(object sender, MouseEventArgs e)
        {
            UIToPLCStructure.ResetAllAxis = false;
        }

        #endregion

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




        //Configure Kinematics Group
        private void btnConfKinGroup_Click(object sender, EventArgs e)
        {
            if (UIToPLCStructure.ConfigKinematicGroup == false)
            {
                UIToPLCStructure.ConfigKinematicGroup = true;
            }
            else
            {
                UIToPLCStructure.ConfigKinematicGroup = false;
            }
        }

        //Reset Kinematics Group
        private void btnResetKinGroup_Click(object sender, EventArgs e)
        {
            if (UIToPLCStructure.ResetKinematicGroup == false)
            {
                UIToPLCStructure.ResetKinematicGroup = true;
            }
            else
            {
                UIToPLCStructure.ResetKinematicGroup = false;
            }
        }


 
    }
}
