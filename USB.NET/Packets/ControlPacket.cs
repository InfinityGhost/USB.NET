namespace USB.NET.Packets
{
    public unsafe struct ControlPacket
    {
        public RequestType bRequestType;
        public Request bRequest;
        public ushort wValue;
        public ushort wIndex;
        public ushort wLength;
        public void* data;
    }
}