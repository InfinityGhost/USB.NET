using System.Runtime.InteropServices;

namespace Native.Linux.Kernel.USB
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct usbfs_ctrltransfer
    {
        public byte bRequestType;
        public byte bRequest;
        public ushort wValue;
        public ushort wIndex;
        public ushort wLength;
        public uint timeout;
        public void* data;
    }
}