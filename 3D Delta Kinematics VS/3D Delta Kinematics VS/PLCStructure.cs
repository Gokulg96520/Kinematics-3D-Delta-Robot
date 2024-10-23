using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace _3D_Delta_Kinematics_VS
{
    class PLCStructure
    {
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class InputStructure
        {
            [MarshalAs(UnmanagedType.R4)]
            public float X_MCSPos;
            public float Y_MCSPos;
            public float Z_MCSPos;
            public float M1_ACSPos;
            public float M2_ACSPos;
            public float M3_ACSPos;
            [MarshalAs(UnmanagedType.I1)]
            public float AllAxisEnabled;
            public float AxisError;
            public float KinematicGroupError;
            public float KinematicGroupingDone;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class OutputStructure
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool EnableAllAxis;
            public bool ConfigKinematicGroup;
            public bool ResetKinematicGroup;
            public bool XJogPositive;
            public bool YJogPositive;
            public bool ZJogPositive;
            public bool XJogNegative;
            public bool YJogNegative;
            public bool ZJogNegative;
        }

    }
}
