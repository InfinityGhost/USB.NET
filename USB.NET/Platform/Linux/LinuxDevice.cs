namespace USB.NET.Platform.Linux
{
    public sealed class LinuxDevice : Device
    {
        public override ushort VendorID => throw new global::System.NotImplementedException();
        public override ushort ProductID => throw new global::System.NotImplementedException();

        public override string Manufacturer => throw new global::System.NotImplementedException();
        public override string ProductName => throw new global::System.NotImplementedException();

        public override string SerialNumber => throw new global::System.NotImplementedException();
        public override string GetIndexedString(byte index)
        {
            throw new global::System.NotImplementedException();
        }

        public override uint ConfigurationCount => throw new global::System.NotImplementedException();

        public override void SetConfiguration(uint index, bool enabled)
        {
            throw new global::System.NotImplementedException();
        }

        public override Configuration GetConfiguration(uint index)
        {
            throw new global::System.NotImplementedException();
        }
    }
}