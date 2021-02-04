using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Native.Windows
{
    public static class Kernel32
    {
        public const int IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION = 0x220410;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile(
            [MarshalAs(UnmanagedType.LPWStr)] string filename,
            FileAccess access,
            FileShare share,
            IntPtr securityAttributes,
            FileMode creationDisposition,
            FileAttributes flagsAndAttributes,
            IntPtr templateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CancelIoEx(IntPtr hHandle, IntPtr overlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode,
            IntPtr lpInBuffer, uint nInBufferSize,
            IntPtr lpOutBuffer, uint nOutBufferSize,
            out uint lpBytesReturned, IntPtr lpOverlapped);
    }
}