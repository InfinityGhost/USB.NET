using System.Runtime.InteropServices;

namespace USB.NET.Packets
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ControlPacket
    {
        public RequestType bmRequestType;
        public Request bRequest;
        public ushort wValue;
        public ushort wIndex;
        public ushort wLength;
        public void* data;
    }
}