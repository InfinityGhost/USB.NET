using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Native.Windows
{
    public static partial class CfgMgr
    {
        public class CfgMgrException : Exception
        {
            public CfgMgrException(string msg, CR status)
                : base(FormatMessage(msg, status))
            {

            }

            public static string FormatMessage(string msg, CR status)
            {
                return $"{msg}: {(int)status} {Enum.GetName(status)}";
            }
        }
    }
}