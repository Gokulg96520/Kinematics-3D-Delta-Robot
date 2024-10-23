
namespace _3D_Delta_Kinematics_VS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl = new OpenTK.GLControl();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbMCS = new System.Windows.Forms.GroupBox();
            this.tbZCord = new System.Windows.Forms.TextBox();
            this.tbYCord = new System.Windows.Forms.TextBox();
            this.tbXCord = new System.Windows.Forms.TextBox();
            this.lbZCord = new System.Windows.Forms.Label();
            this.lbYCord = new System.Windows.Forms.Label();
            this.lbXCord = new System.Windows.Forms.Label();
            this.gbACS = new System.Windows.Forms.GroupBox();
            this.tbM3Cord = new System.Windows.Forms.TextBox();
            this.tbM2Cord = new System.Windows.Forms.TextBox();
            this.tbM1Cord = new System.Windows.Forms.TextBox();
            this.lbM3Cord = new System.Windows.Forms.Label();
            this.lbM2Cord = new System.Windows.Forms.Label();
            this.lbM1Cord = new System.Windows.Forms.Label();
            this.lblAMSNetID = new System.Windows.Forms.Label();
            this.tbAMSNetID = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnEnableAxis = new System.Windows.Forms.Button();
            this.btnConfKinGroup = new System.Windows.Forms.Button();
            this.btnResetAxis = new System.Windows.Forms.Button();
            this.btnResetKinGroup = new System.Windows.Forms.Button();
            this.gbJog = new System.Windows.Forms.GroupBox();
            this.btnZPos = new System.Windows.Forms.Button();
            this.btnZNeg = new System.Windows.Forms.Button();
            this.btnYNeg = new System.Windows.Forms.Button();
            this.btnYPos = new System.Windows.Forms.Button();
            this.btnXPos = new System.Windows.Forms.Button();
            this.btnXNeg = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbMCS.SuspendLayout();
            this.gbACS.SuspendLayout();
            this.gbJog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point(254, 2);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(545, 490);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLeft.AutoSize = true;
            this.btnMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLeft.Location = new System.Drawing.Point(682, 457);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(35, 35);
            this.btnMoveLeft.TabIndex = 1;
            this.btnMoveLeft.Text = "←";
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveRight.AutoSize = true;
            this.btnMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveRight.Location = new System.Drawing.Point(723, 457);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(35, 35);
            this.btnMoveRight.TabIndex = 1;
            this.btnMoveRight.Text = "→";
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.AutoSize = true;
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.Location = new System.Drawing.Point(764, 377);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(35, 35);
            this.btnMoveUp.TabIndex = 1;
            this.btnMoveUp.Text = "↑";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.AutoSize = true;
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.Location = new System.Drawing.Point(764, 417);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(35, 35);
            this.btnMoveDown.TabIndex = 1;
            this.btnMoveDown.Text = "↓";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.AutoSize = true;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(764, 457);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(35, 35);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "R";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // gbMCS
            // 
            this.gbMCS.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbMCS.Controls.Add(this.tbZCord);
            this.gbMCS.Controls.Add(this.tbYCord);
            this.gbMCS.Controls.Add(this.tbXCord);
            this.gbMCS.Controls.Add(this.lbZCord);
            this.gbMCS.Controls.Add(this.lbYCord);
            this.gbMCS.Controls.Add(this.lbXCord);
            this.gbMCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMCS.ForeColor = System.Drawing.Color.White;
            this.gbMCS.Location = new System.Drawing.Point(259, 4);
            this.gbMCS.Name = "gbMCS";
            this.gbMCS.Size = new System.Drawing.Size(126, 85);
            this.gbMCS.TabIndex = 3;
            this.gbMCS.TabStop = false;
            this.gbMCS.Text = "MCS Position";
            // 
            // tbZCord
            // 
            this.tbZCord.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbZCord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbZCord.ForeColor = System.Drawing.Color.Blue;
            this.tbZCord.Location = new System.Drawing.Point(46, 61);
            this.tbZCord.Name = "tbZCord";
            this.tbZCord.Size = new System.Drawing.Size(66, 17);
            this.tbZCord.TabIndex = 5;
            // 
            // tbYCord
            // 
            this.tbYCord.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbYCord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbYCord.ForeColor = System.Drawing.Color.DarkMagenta;
            this.tbYCord.Location = new System.Drawing.Point(46, 41);
            this.tbYCord.Name = "tbYCord";
            this.tbYCord.Size = new System.Drawing.Size(66, 17);
            this.tbYCord.TabIndex = 4;
            // 
            // tbXCord
            // 
            this.tbXCord.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbXCord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbXCord.ForeColor = System.Drawing.Color.Yellow;
            this.tbXCord.Location = new System.Drawing.Point(46, 21);
            this.tbXCord.Name = "tbXCord";
            this.tbXCord.Size = new System.Drawing.Size(66, 17);
            this.tbXCord.TabIndex = 3;
            // 
            // lbZCord
            // 
            this.lbZCord.AutoSize = true;
            this.lbZCord.ForeColor = System.Drawing.Color.Blue;
            this.lbZCord.Location = new System.Drawing.Point(11, 61);
            this.lbZCord.Name = "lbZCord";
            this.lbZCord.Size = new System.Drawing.Size(33, 18);
            this.lbZCord.TabIndex = 2;
            this.lbZCord.Text = "Z  :";
            // 
            // lbYCord
            // 
            this.lbYCord.AutoSize = true;
            this.lbYCord.ForeColor = System.Drawing.Color.DarkMagenta;
            this.lbYCord.Location = new System.Drawing.Point(11, 40);
            this.lbYCord.Name = "lbYCord";
            this.lbYCord.Size = new System.Drawing.Size(33, 18);
            this.lbYCord.TabIndex = 1;
            this.lbYCord.Text = "Y  :";
            // 
            // lbXCord
            // 
            this.lbXCord.AutoSize = true;
            this.lbXCord.ForeColor = System.Drawing.Color.Yellow;
            this.lbXCord.Location = new System.Drawing.Point(10, 20);
            this.lbXCord.Name = "lbXCord";
            this.lbXCord.Size = new System.Drawing.Size(34, 18);
            this.lbXCord.TabIndex = 0;
            this.lbXCord.Text = "X  :";
            // 
            // gbACS
            // 
            this.gbACS.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbACS.Controls.Add(this.tbM3Cord);
            this.gbACS.Controls.Add(this.tbM2Cord);
            this.gbACS.Controls.Add(this.tbM1Cord);
            this.gbACS.Controls.Add(this.lbM3Cord);
            this.gbACS.Controls.Add(this.lbM2Cord);
            this.gbACS.Controls.Add(this.lbM1Cord);
            this.gbACS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbACS.ForeColor = System.Drawing.Color.White;
            this.gbACS.Location = new System.Drawing.Point(259, 90);
            this.gbACS.Name = "gbACS";
            this.gbACS.Size = new System.Drawing.Size(126, 85);
            this.gbACS.TabIndex = 4;
            this.gbACS.TabStop = false;
            this.gbACS.Text = "ACS Position";
            // 
            // tbM3Cord
            // 
            this.tbM3Cord.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbM3Cord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbM3Cord.ForeColor = System.Drawing.Color.White;
            this.tbM3Cord.Location = new System.Drawing.Point(46, 61);
            this.tbM3Cord.Name = "tbM3Cord";
            this.tbM3Cord.Size = new System.Drawing.Size(66, 17);
            this.tbM3Cord.TabIndex = 5;
            // 
            // tbM2Cord
            // 
            this.tbM2Cord.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbM2Cord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbM2Cord.ForeColor = System.Drawing.Color.White;
            this.tbM2Cord.Location = new System.Drawing.Point(46, 41);
            this.tbM2Cord.Name = "tbM2Cord";
            this.tbM2Cord.Size = new System.Drawing.Size(66, 17);
            this.tbM2Cord.TabIndex = 4;
            // 
            // tbM1Cord
            // 
            this.tbM1Cord.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbM1Cord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbM1Cord.ForeColor = System.Drawing.Color.White;
            this.tbM1Cord.Location = new System.Drawing.Point(46, 21);
            this.tbM1Cord.Name = "tbM1Cord";
            this.tbM1Cord.Size = new System.Drawing.Size(66, 17);
            this.tbM1Cord.TabIndex = 3;
            // 
            // lbM3Cord
            // 
            this.lbM3Cord.AutoSize = true;
            this.lbM3Cord.ForeColor = System.Drawing.Color.White;
            this.lbM3Cord.Location = new System.Drawing.Point(4, 61);
            this.lbM3Cord.Name = "lbM3Cord";
            this.lbM3Cord.Size = new System.Drawing.Size(41, 18);
            this.lbM3Cord.TabIndex = 2;
            this.lbM3Cord.Text = "M3 :";
            // 
            // lbM2Cord
            // 
            this.lbM2Cord.AutoSize = true;
            this.lbM2Cord.ForeColor = System.Drawing.Color.White;
            this.lbM2Cord.Location = new System.Drawing.Point(4, 40);
            this.lbM2Cord.Name = "lbM2Cord";
            this.lbM2Cord.Size = new System.Drawing.Size(41, 18);
            this.lbM2Cord.TabIndex = 1;
            this.lbM2Cord.Text = "M2 :";
            // 
            // lbM1Cord
            // 
            this.lbM1Cord.AutoSize = true;
            this.lbM1Cord.ForeColor = System.Drawing.Color.White;
            this.lbM1Cord.Location = new System.Drawing.Point(4, 20);
            this.lbM1Cord.Name = "lbM1Cord";
            this.lbM1Cord.Size = new System.Drawing.Size(41, 18);
            this.lbM1Cord.TabIndex = 0;
            this.lbM1Cord.Text = "M1 :";
            // 
            // lblAMSNetID
            // 
            this.lblAMSNetID.AutoSize = true;
            this.lblAMSNetID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAMSNetID.Location = new System.Drawing.Point(4, 12);
            this.lblAMSNetID.Name = "lblAMSNetID";
            this.lblAMSNetID.Size = new System.Drawing.Size(85, 18);
            this.lblAMSNetID.TabIndex = 5;
            this.lblAMSNetID.Text = "AMS Net ID";
            // 
            // tbAMSNetID
            // 
            this.tbAMSNetID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAMSNetID.Location = new System.Drawing.Point(90, 9);
            this.tbAMSNetID.Name = "tbAMSNetID";
            this.tbAMSNetID.Size = new System.Drawing.Size(158, 24);
            this.tbAMSNetID.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(38, 39);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(86, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(130, 39);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(86, 23);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnEnableAxis
            // 
            this.btnEnableAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnableAxis.Location = new System.Drawing.Point(6, 74);
            this.btnEnableAxis.Name = "btnEnableAxis";
            this.btnEnableAxis.Size = new System.Drawing.Size(158, 23);
            this.btnEnableAxis.TabIndex = 9;
            this.btnEnableAxis.Text = "Enable / Disable  Axis";
            this.btnEnableAxis.UseVisualStyleBackColor = true;
            this.btnEnableAxis.Click += new System.EventHandler(this.btnEnableAxis_Click);
            // 
            // btnConfKinGroup
            // 
            this.btnConfKinGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfKinGroup.Location = new System.Drawing.Point(14, 111);
            this.btnConfKinGroup.Name = "btnConfKinGroup";
            this.btnConfKinGroup.Size = new System.Drawing.Size(226, 23);
            this.btnConfKinGroup.TabIndex = 10;
            this.btnConfKinGroup.Text = "Configure Kinematic Group ";
            this.btnConfKinGroup.UseVisualStyleBackColor = true;
            this.btnConfKinGroup.Click += new System.EventHandler(this.btnConfKinGroup_Click);
            // 
            // btnResetAxis
            // 
            this.btnResetAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetAxis.Location = new System.Drawing.Point(168, 74);
            this.btnResetAxis.Name = "btnResetAxis";
            this.btnResetAxis.Size = new System.Drawing.Size(80, 23);
            this.btnResetAxis.TabIndex = 11;
            this.btnResetAxis.Text = "Reset Axis";
            this.btnResetAxis.UseVisualStyleBackColor = true;
            this.btnResetAxis.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnResetAxis_MouseDown);
            this.btnResetAxis.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnResetAxis_MouseUp);
            // 
            // btnResetKinGroup
            // 
            this.btnResetKinGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetKinGroup.Location = new System.Drawing.Point(14, 140);
            this.btnResetKinGroup.Name = "btnResetKinGroup";
            this.btnResetKinGroup.Size = new System.Drawing.Size(226, 23);
            this.btnResetKinGroup.TabIndex = 12;
            this.btnResetKinGroup.Text = "Reset Kinematic Group ";
            this.btnResetKinGroup.UseVisualStyleBackColor = true;
            this.btnResetKinGroup.Click += new System.EventHandler(this.btnResetKinGroup_Click);
            // 
            // gbJog
            // 
            this.gbJog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbJog.BackColor = System.Drawing.SystemColors.Control;
            this.gbJog.Controls.Add(this.btnZPos);
            this.gbJog.Controls.Add(this.btnZNeg);
            this.gbJog.Controls.Add(this.btnYNeg);
            this.gbJog.Controls.Add(this.btnYPos);
            this.gbJog.Controls.Add(this.btnXPos);
            this.gbJog.Controls.Add(this.btnXNeg);
            this.gbJog.Controls.Add(this.pictureBox1);
            this.gbJog.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbJog.ForeColor = System.Drawing.Color.Black;
            this.gbJog.Location = new System.Drawing.Point(14, 222);
            this.gbJog.Name = "gbJog";
            this.gbJog.Size = new System.Drawing.Size(225, 264);
            this.gbJog.TabIndex = 6;
            this.gbJog.TabStop = false;
            this.gbJog.Text = "Jog MCS";
            // 
            // btnZPos
            // 
            this.btnZPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZPos.AutoSize = true;
            this.btnZPos.BackColor = System.Drawing.Color.Blue;
            this.btnZPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZPos.ForeColor = System.Drawing.Color.White;
            this.btnZPos.Location = new System.Drawing.Point(10, 87);
            this.btnZPos.Name = "btnZPos";
            this.btnZPos.Size = new System.Drawing.Size(37, 30);
            this.btnZPos.TabIndex = 39;
            this.btnZPos.Text = "Z ↑";
            this.btnZPos.UseVisualStyleBackColor = false;
            // 
            // btnZNeg
            // 
            this.btnZNeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZNeg.AutoSize = true;
            this.btnZNeg.BackColor = System.Drawing.Color.Blue;
            this.btnZNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZNeg.ForeColor = System.Drawing.Color.White;
            this.btnZNeg.Location = new System.Drawing.Point(10, 123);
            this.btnZNeg.Name = "btnZNeg";
            this.btnZNeg.Size = new System.Drawing.Size(37, 30);
            this.btnZNeg.TabIndex = 38;
            this.btnZNeg.Text = "Z ↓";
            this.btnZNeg.UseVisualStyleBackColor = false;
            // 
            // btnYNeg
            // 
            this.btnYNeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYNeg.AutoSize = true;
            this.btnYNeg.BackColor = System.Drawing.Color.DarkMagenta;
            this.btnYNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYNeg.ForeColor = System.Drawing.Color.White;
            this.btnYNeg.Location = new System.Drawing.Point(61, 146);
            this.btnYNeg.Name = "btnYNeg";
            this.btnYNeg.Size = new System.Drawing.Size(38, 30);
            this.btnYNeg.TabIndex = 37;
            this.btnYNeg.Text = "↙ Y";
            this.btnYNeg.UseVisualStyleBackColor = false;
            // 
            // btnYPos
            // 
            this.btnYPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYPos.AutoSize = true;
            this.btnYPos.BackColor = System.Drawing.Color.DarkMagenta;
            this.btnYPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYPos.ForeColor = System.Drawing.Color.White;
            this.btnYPos.Location = new System.Drawing.Point(120, 123);
            this.btnYPos.Name = "btnYPos";
            this.btnYPos.Size = new System.Drawing.Size(38, 30);
            this.btnYPos.TabIndex = 36;
            this.btnYPos.Text = "Y ↗";
            this.btnYPos.UseVisualStyleBackColor = false;
            // 
            // btnXPos
            // 
            this.btnXPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXPos.AutoSize = true;
            this.btnXPos.BackColor = System.Drawing.Color.Yellow;
            this.btnXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXPos.ForeColor = System.Drawing.Color.Black;
            this.btnXPos.Location = new System.Drawing.Point(120, 198);
            this.btnXPos.Name = "btnXPos";
            this.btnXPos.Size = new System.Drawing.Size(38, 30);
            this.btnXPos.TabIndex = 35;
            this.btnXPos.Text = "X ↘";
            this.btnXPos.UseVisualStyleBackColor = false;
            // 
            // btnXNeg
            // 
            this.btnXNeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXNeg.AutoSize = true;
            this.btnXNeg.BackColor = System.Drawing.Color.Yellow;
            this.btnXNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXNeg.ForeColor = System.Drawing.Color.Black;
            this.btnXNeg.Location = new System.Drawing.Point(61, 182);
            this.btnXNeg.Name = "btnXNeg";
            this.btnXNeg.Size = new System.Drawing.Size(38, 30);
            this.btnXNeg.TabIndex = 34;
            this.btnXNeg.Text = "↖ X";
            this.btnXNeg.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::_3D_Delta_Kinematics_VS.Properties.Resources.CoOrdinate;
            this.pictureBox1.Location = new System.Drawing.Point(18, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 215);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 494);
            this.Controls.Add(this.gbJog);
            this.Controls.Add(this.btnResetKinGroup);
            this.Controls.Add(this.btnResetAxis);
            this.Controls.Add(this.btnConfKinGroup);
            this.Controls.Add(this.btnEnableAxis);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbAMSNetID);
            this.Controls.Add(this.lblAMSNetID);
            this.Controls.Add(this.gbACS);
            this.Controls.Add(this.gbMCS);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnMoveRight);
            this.Controls.Add(this.btnMoveLeft);
            this.Controls.Add(this.glControl);
            this.Name = "MainForm";
            this.Text = "3D Delta Robot Simulator";
            this.gbMCS.ResumeLayout(false);
            this.gbMCS.PerformLayout();
            this.gbACS.ResumeLayout(false);
            this.gbACS.PerformLayout();
            this.gbJog.ResumeLayout(false);
            this.gbJog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox gbMCS;
        private System.Windows.Forms.TextBox tbZCord;
        private System.Windows.Forms.TextBox tbYCord;
        private System.Windows.Forms.TextBox tbXCord;
        private System.Windows.Forms.Label lbZCord;
        private System.Windows.Forms.Label lbYCord;
        private System.Windows.Forms.Label lbXCord;
        private System.Windows.Forms.GroupBox gbACS;
        private System.Windows.Forms.TextBox tbM3Cord;
        private System.Windows.Forms.TextBox tbM2Cord;
        private System.Windows.Forms.TextBox tbM1Cord;
        private System.Windows.Forms.Label lbM3Cord;
        private System.Windows.Forms.Label lbM2Cord;
        private System.Windows.Forms.Label lbM1Cord;
        private System.Windows.Forms.Label lblAMSNetID;
        private System.Windows.Forms.TextBox tbAMSNetID;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnEnableAxis;
        private System.Windows.Forms.Button btnConfKinGroup;
        private System.Windows.Forms.Button btnResetAxis;
        private System.Windows.Forms.Button btnResetKinGroup;
        private System.Windows.Forms.GroupBox gbJog;
        private System.Windows.Forms.Button btnZPos;
        private System.Windows.Forms.Button btnZNeg;
        private System.Windows.Forms.Button btnYNeg;
        private System.Windows.Forms.Button btnYPos;
        private System.Windows.Forms.Button btnXPos;
        private System.Windows.Forms.Button btnXNeg;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

