using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _3D_Delta_Kinematics_VS
{
    class RobotComponent
    {
        static vec3 baseTrans = new vec3(-0.5f, 0, 0);

        public void DrawCircle()
        {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            int i = 0;
            for (i = 0; i <= 390; i += 15)
            {
                float p = (float)(i * 3.14 / 180);
                GL.Vertex3(Math.Sin(p), Math.Cos(p), 0.0f);
            }
            GL.End();
            GL.PopMatrix();
        }

        public void DrawUnitCylinder()
        {
            int i;
            float[] Px = new float[390];
            float[] Py = new float[390];
            float AngIncr = (2.0f * 3.1415927f) / (float)390;
            float Ang = AngIncr;
            Px[0] = 1;
            Py[0] = 0;
            for (i = 1; i < 390; i++, Ang += AngIncr)
            {
                Px[i] = (float)Math.Cos(Ang);
                Py[i] = (float)Math.Sin(Ang);
            }
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                GL.Translate(0.5f, 0.5f, 0.5f);
                GL.Scale(0.5f, 0.5f, 0.5f);
                GL.Normal3(0, 1, 0);
                GL.Begin(PrimitiveType.TriangleFan);
                GL.Vertex3(0, 1, 0);
                for (i = 0; i < 390; i++)
                    GL.Vertex3(Px[i], 1, -Py[i]);
                GL.Vertex3(Px[0], 1, -Py[0]);
                GL.End();
                // Bottom
                GL.Normal3(0, -1, 0);
                GL.Begin(PrimitiveType.TriangleFan);
                GL.Vertex3(0, -1, 0);
                for (i = 0; i < 390; i++)
                    GL.Vertex3(Px[i], -1, Py[i]);
                GL.Vertex3(Px[0], -1, Py[0]);
                GL.End();
                // Sides
                GL.Begin(PrimitiveType.QuadStrip);
                for (i = 0; i < 390; i++)
                {
                    GL.Normal3(Px[i], 0, -Py[i]);
                    GL.Vertex3(Px[i], 1, -Py[i]);
                    GL.Vertex3(Px[i], -1, -Py[i]);
                }
                GL.Normal3(Px[0], 0, -Py[0]);
                GL.Vertex3(Px[0], 1, -Py[0]);
                GL.Vertex3(Px[0], -1, -Py[0]);
                GL.End();
            }
            GL.PopMatrix();

        }

        public void DrawUnitCylinder(vec3 vTop, vec3 vBottom)
        {
            int i;

            float[] Px = new float[390];
            float[] Py = new float[390];
            float[] Qx = new float[390];
            float[] Qy = new float[390];

            float AngIncr = (float)(2 * Math.PI) / (float)390;
            float Ang = AngIncr;

            Px[0] = 0.5f + vTop.x;
            Py[0] = 0 + vTop.y;
            Qx[0] = 0.5f + vBottom.x;
            Qy[0] = 0 + vBottom.y;
            for (i = 1; i < 390; i++, Ang += AngIncr)
            {
                Px[i] = (float)Math.Cos(Ang) / 2 + vTop.x;
                Py[i] = (float)Math.Sin(Ang) / 2 + vTop.y;
                Qx[i] = (float)Math.Cos(Ang) / 2 + vBottom.x;
                Qy[i] = (float)Math.Sin(Ang) / 2 + vBottom.y;
            }

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                //GL.Translate(0.5f, 0.5f, 0.5f);
                //GL.Scale(0.5f, 0.5f, 0.5f);
                // Sides
                GL.Begin(PrimitiveType.QuadStrip);
                for (i = 0; i < 390; i++)
                {
                    //GL.Normal3(Px[i]-Qx[i], 0, -(Py[i]-Qy[i]));
                    GL.Vertex3(Px[i], vTop.z, -Py[i]);
                    GL.Vertex3(Qx[i], vBottom.z, -Qy[i]);
                }
                //GL.Normal3(Px[0], 0, -Py[0]);
                GL.Vertex3(Px[0], vTop.z, -Py[0]);
                GL.Vertex3(Qx[0], vBottom.z, -Qy[0]);
                GL.End();
            }
            GL.PopMatrix();

        }

        public void DrawUnitSphere(int numSegs, vec3 Pos, double dRadius, int iSegX, int iSegY)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                GL.Translate(Pos.x, Pos.y, Pos.z);
                for (int i = 0; i < iSegY; i++)
                {
                    double lat0 = Math.PI * (-0.5 + (double)(i) / iSegY); // Latitude angle
                    double z0 = dRadius * Math.Sin(lat0);                // Z coordinate
                    double zr0 = dRadius * Math.Cos(lat0);               // Radius at this latitude

                    double lat1 = Math.PI * (-0.5 + (double)(i + 1) / iSegY); // Next latitude angle
                    double z1 = dRadius * Math.Sin(lat1);                     // Next Z coordinate
                    double zr1 = dRadius * Math.Cos(lat1);                    // Next radius

                    GL.Begin(PrimitiveType.QuadStrip);
                    for (int j = 0; j <= iSegX; j++)
                    {
                        double lng = 2 * Math.PI * (double)(j) / iSegX; // Longitude angle
                        double x = Math.Cos(lng);                        // X coordinate
                        double y = Math.Sin(lng);                        // Y coordinate

                        // Vertex for the first latitude
                        GL.Normal3(x * zr0, y * zr0, z0);                // Normal for lighting
                        GL.Vertex3(x * zr0, y * zr0, z0);                // Vertex

                        // Vertex for the next latitude
                        GL.Normal3(x * zr1, y * zr1, z1);                // Normal for lighting
                        GL.Vertex3(x * zr1, y * zr1, z1);                // Vertex
                    }
                    GL.End();
                }


            }
            GL.PopMatrix();
        }

        public void DrawUnitCone()
        {
            //int i;
            //var P = new List<vec3>();
            //float AngIncr = (2.0f * PI) / (float)numSegs;
            //float Ang = AngIncr;
            //for (i = 0; i < numSegs; i++, Ang += AngIncr)
            //{
            //    P.Add(new vec3((float)Math.Cos(Ang),
            //                   (float)Math.Sin(Ang),
            //                   0));
            //}

            ////GL.MatrixMode(MatrixMode.Modelview);
            //GL.PushMatrix();
            //GL.Translate(Pos.x, Pos.y, Pos.z);
            //GL.Normal3(0, fHieght, 0);
            //GL.Begin(PrimitiveType.TriangleFan);
            //GL.Vertex3(Pos.x, Pos.y + fHieght, Pos.z);
            //for (i = 0; i < numSegs; i++)
            //    GL.Vertex3(fRadius*P[i].x, 0, -fRadius*P[i].y);
            //GL.Vertex3(Pos.x,Pos.y,Pos.z);
            //GL.End();
            GL.Rotate(-90, 1, 0, 0);
            GL.Begin(PrimitiveType.QuadStrip);
            int i = 0;
            for (i = 0; i <= 390; i++)
            {
                float p = (float)(i * 3.14 / 180);
                GL.Vertex3(0, 0, 1.0f);
                GL.Vertex3(Math.Sin(p), Math.Cos(p), 0.0f);
            }
            GL.End();
            DrawCircle();

        }

        public void DrawJoint()        //Joints -- Col Ch Ab
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.Scale(1.5f, 1.5f, 1.2f);
            GL.Rotate(90, 1, 0, 0);
            GL.Translate(-0.5f, -0.5f, -0.5f);
            GL.Color3(1.0f, 1.0f, 1);
            DrawUnitCylinder();
            GL.PopMatrix();
        }

        public void DrawBase()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                GL.Scale(2f, 0.2f, 2f);
                GL.Translate(-0.5f, 0, -0.5f);
                GL.Color3(0, 1f, 1f);
                DrawUnitCylinder();
            }

            GL.PopMatrix();
            GL.PushMatrix();
            {
                GL.Translate(-0.5f, 0, -0.5f);
                GL.Scale(1f, 4f, 1f);
                GL.Color3(0.5f, 1.0f, 0.5f);//Color3 arm 
                DrawUnitCylinder();
            }
            GL.PopMatrix();

            GL.PushMatrix();
            {
                GL.Translate(0f, 4f, 0);
                //GL.Scale(4f, 8f, 4f);
                DrawJoint();
            }
            GL.PopMatrix();
        }

        public void DrawArmSegment()
        {
            // GL.MatrixMode(MatrixMode.Modelview);
            // GL.PushMatrix();
            // GL.Translate(-0.5f, 0, -0.5f);
            // GL.Scale(1f, 5f, 1f);
            // GL.Color3(1.0f, 1f, 1.0f);//Color3 arm 
            // DrawUnitCylinder(ref GL);
            // GL.PopMatrix();
            //
            // GL.PushMatrix();
            // GL.Translate(0, 5.5f, 0);
            // DrawJoint(ref GL);
            // GL.PopMatrix();
        }

        public void DrawWrist()        //Usx
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                GL.Color3(0f, 1f, 0f);
                GL.Translate(-0.4f, 0, -0.4f);
                GL.Scale(0.8f, 2f, 0.8f);

                DrawUnitCylinder();
            }
            GL.PopMatrix();

            GL.PushMatrix();
            {
                GL.Translate(0, 0.2f, 0);
                //GL.Scale(1, 1.2f, 1.2f);
                GL.Translate(0f, 2f, 0f);
                GL.Color3(1, 0, 1);
                DrawUnitSphere( 128, new vec3(0, 0, 0), 0.8, 128, 128);
            }

            GL.PopMatrix();
        }

        public void DrawFingerBase()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                GL.Translate(-0.25f, 0, -0.25f);
                GL.Scale(0.5f, 3f, 0.5f);
                GL.Color3(1.0f, 1f, 1);
                DrawUnitCylinder();
            }
            GL.PopMatrix();

            GL.PushMatrix();
            {
                GL.Translate(0, 0.3f, 0);
                //GL.Scale(0.08f, 0.08f, 0.08f);
                GL.Translate(0f, 3f, 0f);
                GL.Color3(0.5, 0.5f, 0.5f);
                DrawUnitSphere(128, new vec3(0, 0, 0), 0.5, 128, 128);
            }
            GL.PopMatrix();
        }

        public void DrawFingerTip()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            {
                GL.Scale(0.25f, 2f, 0.25f);
                GL.Translate(-0.5f, 0, -0.5f);
                GL.Color3(0, 1f, 0.5f);
                DrawUnitCone();
            }
            GL.PopMatrix();
        }

        public void DrawRobotArm()
        {
            // GL.MatrixMode(MatrixMode.Modelview);
            // GL.Translate(baseTrans.x, 0, baseTrans.z);
            // GL.Rotate(baseSpin, 0f, 360f, 0f);
            // DrawBase(ref GL);
            //
            // GL.Translate(0f, 4.0f, 0f);
            // GL.Rotate(15f, 0f, 0f, 90f);
            // DrawArmSegment(ref GL);
            //
            // GL.Translate(0, 5.5f, 0);
            // GL.Rotate(120f, 0f, 0f, 90f);
            // DrawArmSegment(ref GL);
            //
            // GL.Translate(0, 5.5f, 0);
            // GL.Rotate(-90.0f, 0f, 0f, 90f);
            // DrawWrist(ref GL);
            //
            // GL.Rotate(10, 0.0f, 180f, 0f);
            //
            // GL.PushMatrix();
            // {
            //     GL.Translate(0f, 2f, 0f);
            //     GL.Rotate(45, 0f, 0f, -180f);
            //     DrawFingerBase(ref GL);
            //     GL.Translate(0f, 3.25f, 0f);
            //     GL.Rotate(-90, 0f, 0f, -90f);
            //     DrawFingerTip(ref GL);
            // }
            // GL.PopMatrix();
            //
            // GL.PushMatrix();
            // {
            //     GL.Translate(0f, 2f, 0f);
            //     GL.Rotate(45, 0f, 0f, 90f);
            //     DrawFingerBase(ref GL);
            //     GL.Translate(0f, 3.25f, 0);
            //     GL.Rotate(-90, 0f, 0f, 90f);
            //     DrawFingerTip(ref GL);
            // }
            // GL.PopMatrix();
        }


    }
}
