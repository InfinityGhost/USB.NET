using Native.Linux.libc;
using Native.Linux.Kernel.USB;
using System.IO;
using System.Runtime.InteropServices;

namespace USB.NET.Platform.Linux
{
    using static libcMethods;

    internal static class Tools
    {
        public static usbfs_ctrltransfer ControlRequest(string devname, usbfs_ctrltransfer setup)
        {
            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd == -1 || ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                throw CreateIOExceptionFromLastError(fd);
            close(fd);
            return setup;
        }

        public static IOException CreateIOExceptionFromLastError(int fd = -1)
        {
            if (fd != -1)
                libcMethods.close(fd);
            var errno = (Error)Marshal.GetLastWin32Error();
            return new IOException(errno.ToString());
        }
    }
}