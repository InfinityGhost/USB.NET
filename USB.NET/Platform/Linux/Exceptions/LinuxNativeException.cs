using System;
using Native.Linux.libc;

namespace USB.NET.Platform.Linux.Exceptions
{
    public class LinuxNativeException : Exception
    {
        public LinuxNativeException(Error error)
            : base($"{error}")
        {
            Error = error;
        }

        public Error Error { get; }
    }
}