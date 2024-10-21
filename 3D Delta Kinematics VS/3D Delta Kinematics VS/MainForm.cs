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

namespace _3D_Delta_Kinematics_VS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeGLComponent();

        }

        private void InitializeGLComponent()
        {
            glControl.Load += GLControl_Load;
            glControl.Paint += GLControl_Paint;
            glControl.Resize += GLControl_Resize;

        }
        private void GLControl_Load(object sender, EventArgs e)
        {

            // Enable blending for transparency
            GL.Enable(EnableCap.DepthTest);

            // Set the clear color
            GL.ClearColor(Color.FromArgb(0, 0, 0, 0)); // Fully transparent red
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            // Clear the color and depth buffers
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Add your rendering code here


            // Swap the buffers to display the rendered image
            glControl.SwapBuffers();
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.MatrixMode(MatrixMode.Modelview);

            //This will call GLControl_Paint
            glControl.Invalidate();
        }


    }
}
