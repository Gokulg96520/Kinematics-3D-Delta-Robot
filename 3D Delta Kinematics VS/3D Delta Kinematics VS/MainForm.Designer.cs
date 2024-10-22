
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
            this.zCameraMovement = new System.Windows.Forms.TrackBar();
            this.xCameraMovement = new System.Windows.Forms.TrackBar();
            this.yCameraMovement = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.zCameraMovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCameraMovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCameraMovement)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point(211, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(589, 450);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            // 
            // zCameraMovement
            // 
            this.zCameraMovement.BackColor = System.Drawing.Color.Black;
            this.zCameraMovement.CausesValidation = false;
            this.zCameraMovement.Location = new System.Drawing.Point(12, 10);
            this.zCameraMovement.Minimum = -10;
            this.zCameraMovement.Name = "zCameraMovement";
            this.zCameraMovement.Size = new System.Drawing.Size(141, 45);
            this.zCameraMovement.TabIndex = 1;
            this.zCameraMovement.Tag = "";
            this.zCameraMovement.ValueChanged += new System.EventHandler(this.zCameraMovement_ValueChanged);
            // 
            // xCameraMovement
            // 
            this.xCameraMovement.BackColor = System.Drawing.Color.Black;
            this.xCameraMovement.CausesValidation = false;
            this.xCameraMovement.Location = new System.Drawing.Point(12, 61);
            this.xCameraMovement.Minimum = -10;
            this.xCameraMovement.Name = "xCameraMovement";
            this.xCameraMovement.Size = new System.Drawing.Size(141, 45);
            this.xCameraMovement.TabIndex = 1;
            this.xCameraMovement.ValueChanged += new System.EventHandler(this.xCameraMovement_ValueChanged);
            // 
            // yCameraMovement
            // 
            this.yCameraMovement.BackColor = System.Drawing.Color.Black;
            this.yCameraMovement.CausesValidation = false;
            this.yCameraMovement.Location = new System.Drawing.Point(12, 112);
            this.yCameraMovement.Minimum = -10;
            this.yCameraMovement.Name = "yCameraMovement";
            this.yCameraMovement.Size = new System.Drawing.Size(141, 45);
            this.yCameraMovement.TabIndex = 1;
            this.yCameraMovement.ValueChanged += new System.EventHandler(this.yCameraMovement_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.yCameraMovement);
            this.Controls.Add(this.xCameraMovement);
            this.Controls.Add(this.zCameraMovement);
            this.Controls.Add(this.glControl);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.zCameraMovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCameraMovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCameraMovement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.TrackBar zCameraMovement;
        private System.Windows.Forms.TrackBar xCameraMovement;
        private System.Windows.Forms.TrackBar yCameraMovement;
    }
}

