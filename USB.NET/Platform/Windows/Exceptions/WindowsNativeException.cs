using System.ComponentModel;
using System.Runtime.InteropServices;

namespace USB.NET.Platform.Windows.Exceptions
{
    public class WindowsNativeException : Win32Exception
    {
        public WindowsNativeException(string message)
            : base(Marshal.GetLastWin32Error(), $"{message}: {Marshal.GetLastWin32Error()}")
        {

        }
    }
}