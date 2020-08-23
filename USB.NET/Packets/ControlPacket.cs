namespace USB.NET.Packets
{
    public unsafe struct ControlPacket
    {
        public byte bRequestType;
        public byte bRequest;
        public ushort wValue;
        public ushort wIndex;
        public ushort wLength;
        public void* data;
    }
}