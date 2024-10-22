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
        private float _zCameraTranslation = -1.0f;
        private float _xCameraTranslation = 0.0f;
        private float _yCameraTranslation = 0.0f;

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

            // Set the clear color to black
            GL.ClearColor(Color.FromArgb(0, 0, 0, 0));
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            // Add your rendering code here
            Render();

        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
            if (glControl.Width > 0 && glControl.Height > 0)
            {

                GL.Viewport(0, 0, glControl.Width, glControl.Height);

                // Set up a perspective projection matrix
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                    MathHelper.PiOver4, // 45-degree field of view
                    (float)glControl.Width / glControl.Height, // Aspect ratio
                    0.1f, // Near clipping plane
                    100.0f // Far clipping plane
                );
                GL.LoadMatrix(ref perspective);
                GL.MatrixMode(MatrixMode.Modelview);

            }
        }

        private void Render()
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Clear the color and depth buffers

            GL.LoadIdentity();  // Reset transformations
            // Move the line into view along the Z-axis
            
            GL.Translate(_xCameraTranslation, _yCameraTranslation, _zCameraTranslation);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(1.0f, 0.0f, 0.0f);  // Set line color to red
            GL.Vertex3(-1.0f, 0.0f, 0.0f);  // Start point of the line
            GL.Vertex3(1.0f, 0.0f, 0.0f);   // End point of the line
            GL.End();

            glControl.SwapBuffers();  // Swap buffers to display the rendered line


            

        }

        private void zCameraMovement_ValueChanged(object sender, EventArgs e)
        {
            _zCameraTranslation = zCameraMovement.Value;
            glControl.Invalidate();
        }
        private void xCameraMovement_ValueChanged(object sender, EventArgs e)
        {
            _xCameraTranslation = xCameraMovement.Value;
            glControl.Invalidate();
        }
        private void yCameraMovement_ValueChanged(object sender, EventArgs e)
        {
            _yCameraTranslation = yCameraMovement.Value;
            glControl.Invalidate();
        }
    }
}
