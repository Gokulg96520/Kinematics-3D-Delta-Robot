using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _3D_Delta_Kinematics_VS
{
    class Delta_3_Robot
    {
        public static double[] ThetaMotor = new double[3];
        public static double[] ThetaPassiveRy = new double[3];
        public static double[] ThetaPassiveRz = new double[3];
        public static float[] Thita_L3 = new float[3] { 0, 0, 0 };
        public static vec3 GoalPos = new vec3();
        RobotComponent rc = new RobotComponent();
        //Delta Specification
        private float fStaticPlateRadius = 120;
        private float fActiveLeg = 180;
        private float fMovingPlate = 50;

        private float fRatio = 0.03f;

        public Delta_3_Robot(vec3 GPos)
        {
            GoalPos = GPos;
            /*
            GoalPos.x = 0.0f;
            GoalPos.y = -75.0f;
            GoalPos.z = -380.518f;*/
            double[] tmp = new double[] { GoalPos.x, GoalPos.y, GoalPos.z };
            vec3[] a = new vec3[3];
            vec3[] b = new vec3[3];
            InverseKinematic IK = new InverseKinematic();
            IK.delta_calcInverse(tmp[0], tmp[1], tmp[2], ref ThetaMotor, ref ThetaPassiveRy, ref ThetaPassiveRz);
        }

        public void DrawActiveArm()
        {
            GL.PushMatrix();
            GL.Translate(-0.5f, 0, -0.5f);
            //GL.Translate(-0.0f, 0, -0.5f);  // moi sua chieu ngay 31/10, can xem xet
            GL.Scale(1f, fActiveLeg * fRatio, 1f);
            GL.Color3(0.5f, 0.5f, 0.5f);//Gray
            rc.DrawUnitCylinder();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0, fActiveLeg * fRatio + 0.1f, 0); //can xem xet
            //GL.Scale(0.8f, 0.8f, 0.8f);

            rc.DrawJoint();
            GL.PopMatrix();
        }

        public void DrawPassiveArm()
        {
            //GL.PushMatrix();
            //GL.Translate(-0.5f, 0, -0.5f);
            //GL.Scale(1f, fPassiveLeg * fRatio, 1f);
            //GL.Color3(0f, 1.0f, 0.5f);//Color3 arm 
            //rc.DrawUnitCylinder(ref GL);
            //
            GL.PushMatrix();
            InverseKinematic IK = new InverseKinematic();
            vec3[] Top = IK.kinv_TopPos(GoalPos.x / fRatio, GoalPos.y / fRatio, GoalPos.z / fRatio);
            vec3[] Bottom = IK.kinv_BotPos(GoalPos.x / fRatio, GoalPos.y / fRatio, GoalPos.z / fRatio);

            GL.Color3(0.5f, 0.5f, 0.5f);//Gray

            //rc.DrawUnitCylinder(ref GL, fRatio * new vec3(Top[0].y, Top[0].x, Top[0].z), fRatio * new vec3(Bottom[0].y, -Bottom[0].x, Bottom[0].z));
            rc.DrawUnitCylinder(fRatio * new vec3(Top[0].y, Top[0].x, Top[0].z), fRatio * new vec3(Bottom[0].y, -Bottom[0].x, Bottom[0].z));
            //GL.Translate(0.5f, 0, 0.5f);  // moi sua chieu ngay 31/10

            //rc.DrawUnitCylinder(ref GL, fRatio * new vec3(-Top[1].x, -Top[1].y, Top[1].z), fRatio * new vec3(-Bottom[1].x, -Bottom[1].y, Bottom[1].z));
            rc.DrawUnitCylinder( fRatio * new vec3(Top[1].y, Top[1].x, Top[1].z), fRatio * new vec3(Bottom[1].y, -Bottom[1].x, Bottom[1].z));

            //vec3 test1 = fRatio * new vec3(Top[2].x, Top[2].y, Top[2].z);
            //vec3 test2 = fRatio * new vec3(Bottom[2].x, Bottom[2].y, Bottom[2].z);

            vec3 test1 = fRatio * new vec3(Top[2].y, Top[2].x, Top[2].z);
            vec3 test2 = fRatio * new vec3(Bottom[2].y, -Bottom[2].x, Bottom[2].z);
            rc.DrawUnitCylinder(test1, test2);

            GL.PopMatrix();
        }

        public void DrawStaticPlate()
        {
            GL.PushMatrix();
            GL.Translate(-(fStaticPlateRadius * fRatio), 0, -(fStaticPlateRadius * fRatio));
            GL.Scale(fStaticPlateRadius * 2 * fRatio, 0.5f, fStaticPlateRadius * 2 * fRatio);
            GL.Color3(0.5f, 0.5f, 0.5f);//Gray
            rc.DrawUnitCylinder();
            GL.PopMatrix();

            //Leg 1 Joint
            GL.PushMatrix();
            GL.Translate((float)fStaticPlateRadius * fRatio * Math.Sin(-90 * Math.PI / 180),
                            0.5f,
                            (float)fStaticPlateRadius * fRatio * Math.Cos(-90 * Math.PI / 180));
            rc.DrawJoint();
            GL.PopMatrix();

            //Leg 2 Joint
            GL.PushMatrix();
            GL.Translate((float)fStaticPlateRadius * fRatio * Math.Sin(30 * Math.PI / 180),
                    0.5f,
                    (float)fStaticPlateRadius * fRatio * Math.Cos(30 * Math.PI / 180));
            GL.Rotate(120, 0f, 120f, 0f);
            rc.DrawJoint();
            GL.PopMatrix();

            //Leg 3 Joint
            GL.PushMatrix();
            GL.Translate((float)fStaticPlateRadius * fRatio * Math.Sin(150 * Math.PI / 180),
                    0.5f,
                    (float)fStaticPlateRadius * fRatio * Math.Cos(150 * Math.PI / 180));
            GL.Rotate(-120, 0f, 120f, 0f);
            rc.DrawJoint();
            GL.PopMatrix();
        }

        public void DrawMovePlate()
        {
            //rc.DrawUnitCylinder(ref GL, new vec3(0, 0, 0), new vec3(1, 1, 1));

            GL.PushMatrix();
            GL.Translate(-fMovingPlate * fRatio, 0, -fMovingPlate * fRatio);
            GL.Scale(fMovingPlate * 2 * fRatio, 0.5f, fMovingPlate * 2 * fRatio);
            GL.Color3(1f, 0f, 0f);
            rc.DrawUnitCylinder();
            GL.PopMatrix();

            //Leg 1 Joint
            GL.PushMatrix();


            //plateX = (float)fMovingPlate * fRatio * Math.Sin(-90 * Math.PI / 180);


            GL.Translate((float)fMovingPlate * fRatio * Math.Sin(-90 * Math.PI / 180),
                            0.5f,
                            (float)fMovingPlate * fRatio * Math.Cos(-90 * Math.PI / 180));
            GL.Scale(0.6f, 0.6f, 0.6f);
            rc.DrawJoint();
            GL.PopMatrix();

            //Leg 2 Joint
            GL.PushMatrix();
            GL.Translate((float)fMovingPlate * fRatio * Math.Sin(30 * Math.PI / 180),
                    0.5f,
                    (float)fMovingPlate * fRatio * Math.Cos(30 * Math.PI / 180));
            GL.Scale(0.6f, 0.6f, 0.6f);
            GL.Rotate(120, 0f, 120f, 0f);
            rc.DrawJoint();
            GL.PopMatrix();

            //Leg 3 Joint
            GL.PushMatrix();
            GL.Translate((float)fMovingPlate * fRatio * Math.Sin(150 * Math.PI / 180),
                    0.5f,
                    (float)fMovingPlate * fRatio * Math.Cos(150 * Math.PI / 180));
            GL.Scale(0.6f, 0.6f, 0.6f);
            GL.Rotate(-120, 0f, 120f, 0f);
            rc.DrawJoint();
            GL.PopMatrix();


        }

        public void DrawDelta3Robot()
        {
            vec3[] vPassiveTop = new vec3[3];
            vec3[] vPassiveBottom = new vec3[3];

            GL.MatrixMode(MatrixMode.Modelview);
            DrawStaticPlate();

            GL.PushMatrix();
            {
                GoalPos = GoalPos * fRatio;
                GL.Translate(GoalPos.y, GoalPos.z, GoalPos.x);

                DrawMovePlate();
            }
            GL.PopMatrix();

            GL.PushMatrix();
            {
                DrawPassiveArm();
            }
            GL.PopMatrix();

            //Leg 1
            GL.PushMatrix();
            {
                GL.Translate((float)fStaticPlateRadius * fRatio * Math.Sin(-90 * Math.PI / 180),
                            0.5f,
                            (float)fStaticPlateRadius * fRatio * Math.Cos(-90 * Math.PI / 180));
                GL.Rotate(90f + ThetaMotor[0], 0f, 0f, 1f);
                DrawActiveArm();
                GL.Translate(0f, 5.4f, 0f);

            }
            GL.PopMatrix();

            //Leg 2
            GL.PushMatrix();
            {
                GL.Translate((float)fStaticPlateRadius * fRatio * Math.Sin(30 * Math.PI / 180),
                    0.5f,
                    (float)fStaticPlateRadius * fRatio * Math.Cos(30 * Math.PI / 180));
                GL.Rotate(120, 0f, 1f, 0f);
                //GL.Rotate(90f, 0f, 0f, 1f);
                GL.Rotate(90 + ThetaMotor[1], 0f, 0f, 1f);
                DrawActiveArm();

                //GL.Translate(0f, 5.4f, 0f);
                //GL.Rotate(ThetaPassiveRz[1], 0f, 1f, 0f);
                //GL.Rotate(ThetaPassiveRy[1], 0f, 0f, 1f);

                //DrawPassiveArm(ref GL);
            }
            GL.PopMatrix();

            ////Leg 3
            GL.PushMatrix();
            {
                GL.Translate((float)fStaticPlateRadius * fRatio * Math.Sin(150 * Math.PI / 180),
                    0.5f,
                    (float)fStaticPlateRadius * fRatio * Math.Cos(150 * Math.PI / 180));
                GL.Rotate(-120, 0f, 1f, 0f);
                //GL.Rotate(90f, 0f, 0f, 1f);
                GL.Rotate(90 + ThetaMotor[2], 0f, 0f, 1f);
                DrawActiveArm();

                //GL.Translate(0f, 5.4f, 0f);
                //GL.Rotate(ThetaPassiveRz[2], 0f, 1f, 0f);
                //GL.Rotate(ThetaPassiveRy[2], 0f, 0f, 1f);

                //DrawPassiveArm(ref GL);
            }
            GL.PopMatrix();


        }
    }
}
