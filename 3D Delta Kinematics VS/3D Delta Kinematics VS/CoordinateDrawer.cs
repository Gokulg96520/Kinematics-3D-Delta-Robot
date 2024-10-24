﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;


namespace _3D_Delta_Kinematics_VS
{
    class CoordinateDrawer
    {
        public void DrawCoordinateAxes(float length = 5.0f, float width = 4.0f)
        {
            GL.PushAttrib(AttribMask.LineBit);

            // Set the line width for the coordinate axes
            GL.LineWidth(width);

            GL.Begin(PrimitiveType.Lines);

            // Y-Axis (Dark Magenta)
            GL.Color3(0.545f, 0.0f, 0.545f);
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origin
            GL.Vertex3(length, 0.0f, 0.0f); // +X direction

            // Z-Axis (Blue)
            GL.Color3(0.0f, 0.0f, 1.0f);  // Blue
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origin
            GL.Vertex3(0.0f, length, 0.0f); // +Y direction

            // X-Axis (Yellow)
            GL.Color3(1.0f, 1.0f, 0.0f);  // Yellow
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origin
            GL.Vertex3(0.0f, 0.0f, length); // +Z direction

            GL.End();

            // Restore the previous OpenGL state
            GL.PopAttrib();

        }

    }
}
