using Native.Linux.libc;
using Native.Linux.Kernel.USB;
using System.IO;
using System.Runtime.InteropServices;
using USB.NET.Platform.Linux.Exceptions;

namespace USB.NET.Platform.Linux
{
    using static libcMethods;

    internal static class Tools
    {
        public static usbfs_ctrltransfer ControlRequest(string devname, usbfs_ctrltransfer setup)
        {
            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd == -1 || ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                throw CreateNativeException(fd);
            close(fd);
            return setup;
        }

        public static LinuxNativeException CreateNativeException(int fd = -1)
        {
            if (fd != -1)
                libcMethods.close(fd);
            return new LinuxNativeException(GetError());
        }

        public static Error GetError()
        {
            return (Error)Marshal.GetLastWin32Error();
        }
    }
}