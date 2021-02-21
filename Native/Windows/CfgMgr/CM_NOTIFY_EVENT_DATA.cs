using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Native.Windows
{
    public static partial class CfgMgr
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct CM_NOTIFY_EVENT_DATA
        {
            CM_NOTIFY_FILTER_TYPE FILTER_TYPE;
            int Reserved;
            Guid ClassGuid;
            StringBuilder SymbolicLink;
        }
    }
}