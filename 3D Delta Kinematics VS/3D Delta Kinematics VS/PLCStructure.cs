﻿using System;
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
            public float X_MCSPos = 0.0f;
            [MarshalAs(UnmanagedType.R4)]
            public float Y_MCSPos = 0.0f;
            [MarshalAs(UnmanagedType.R4)]
            public float Z_MCSPos = 0.0f;
            [MarshalAs(UnmanagedType.R4)]
            public float M1_ACSPos = 0.0f;
            [MarshalAs(UnmanagedType.R4)]
            public float M2_ACSPos = 0.0f;
            [MarshalAs(UnmanagedType.R4)]
            public float M3_ACSPos = 0.0f;
            [MarshalAs(UnmanagedType.I1)]
            public bool AllAxisEnabled = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool AxisError = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool KinematicGroupError = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool KinematicGroupingReady = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool NCIAxisGrouped = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool NCIAXisError = false;
            [MarshalAs(UnmanagedType.U4)]
            public Int32 NCIInterpreterState = 0;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class OutputStructure
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool EnableAllAxis = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool ResetAllAxis = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool ConfigKinematicGroup = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool ResetKinematicGroup = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool XJogPositive = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool YJogPositive = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool ZJogPositive = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool XJogNegative = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool YJogNegative = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool ZJogNegative = false;
            [MarshalAs(UnmanagedType.R4)]
            public float JogSpeed = 0.0f;
            [MarshalAs(UnmanagedType.I1)]
            public bool NCIAxisGroup  = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool NCIAxisUnGroup = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool NCIInteperatorReset  = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool RunPartProgram  = false;
            [MarshalAs(UnmanagedType.I1)]
            public bool StopPartProgram  = false;
            [MarshalAs(UnmanagedType.R4)]
            public float NCIOverRidePer  = 0.0f;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
            public string PartProgramName = "testProgram.nc";
        }

    }
}
