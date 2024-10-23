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
            [MarshalAs(UnmanagedType.R4)]
            public float Y_MCSPos;
            [MarshalAs(UnmanagedType.R4)]
            public float Z_MCSPos;
            [MarshalAs(UnmanagedType.R4)]
            public float M1_ACSPos;
            [MarshalAs(UnmanagedType.R4)]
            public float M2_ACSPos;
            [MarshalAs(UnmanagedType.R4)]
            public float M3_ACSPos;
            [MarshalAs(UnmanagedType.I1)]
            public Boolean AllAxisEnabled;
            [MarshalAs(UnmanagedType.I1)]
            public Boolean AxisError;
            [MarshalAs(UnmanagedType.I1)]
            public Boolean KinematicGroupError;
            [MarshalAs(UnmanagedType.I1)]
            public Boolean KinematicGroupingDone;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class OutputStructure
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool EnableAllAxis;
            [MarshalAs(UnmanagedType.I1)]
            public bool ResetAllAxis;
            [MarshalAs(UnmanagedType.I1)]
            public bool ConfigKinematicGroup;
            [MarshalAs(UnmanagedType.I1)]
            public bool ResetKinematicGroup;
            [MarshalAs(UnmanagedType.I1)]
            public bool XJogPositive;
            [MarshalAs(UnmanagedType.I1)]
            public bool YJogPositive;
            [MarshalAs(UnmanagedType.I1)]
            public bool ZJogPositive;
            [MarshalAs(UnmanagedType.I1)]
            public bool XJogNegative;
            [MarshalAs(UnmanagedType.I1)]
            public bool YJogNegative;
            [MarshalAs(UnmanagedType.I1)]
            public bool ZJogNegative;
        }

    }
}
