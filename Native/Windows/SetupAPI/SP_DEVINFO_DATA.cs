using System;
using System.Runtime.InteropServices;

namespace Native.Windows
{
    public static partial class SetupAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }
    }
}