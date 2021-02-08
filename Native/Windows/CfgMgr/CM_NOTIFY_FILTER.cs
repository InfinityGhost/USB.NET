using System;
using System.Runtime.InteropServices;

namespace Native.Windows
{
    public static partial class CfgMgr
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct CM_NOTIFY_FILTER
        {
            public int cbSize;
            public CM_NOTIFY_FILTER_FLAGS Flags;
            public CM_NOTIFY_FILTER_TYPE FilterType;
            public Guid ClassGuid;
        }
    }
}