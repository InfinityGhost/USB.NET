using System.Runtime.InteropServices;

namespace USB.NET.Packets
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ControlPacket
    {
        RequestType bmRequestType;
        Request bRequest;
        ushort wValue;
        ushort wIndex;
        ushort wLength;
        void* data;
    }
}