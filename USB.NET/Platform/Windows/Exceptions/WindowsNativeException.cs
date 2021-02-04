using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace USB.NET.Platform.Windows.Exceptions
{
    public class WindowsNativeException : Exception
    {
        public WindowsNativeException()
            : base()
        {

        }

        public WindowsNativeException(string message)
            : base(FormatMessage(message))
        {

        }

        private static string FormatMessage(string message)
        {
            var err = Marshal.GetLastWin32Error();
            return $"{message}: {new Win32Exception(err).Message} (Error {err})";
        }
    }
}