using System;
using System.IO;
using System.Runtime.InteropServices;
using Native.Linux.libc;
using Native.Linux.Linux.USB;
using USB.NET.Descriptors;
using USB.NET.Packets;

namespace USB.NET.Platform.Linux
{
    internal unsafe static class Tools
    {
        public static ushort string_index(byte index)
        {
            return (ushort)(((byte)DescriptorType.String << 8) | index);
        }

        public static IOException CreateIOExceptionFromLastError()
        {
            var errno = (Error)Marshal.GetLastWin32Error();
            return new IOException(errno.ToString());
        }
    }
}