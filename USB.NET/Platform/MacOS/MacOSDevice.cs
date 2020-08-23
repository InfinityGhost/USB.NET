namespace USB.NET.Platform.MacOS
{
    public sealed class MacOSDevice : Device
    {
        public override ushort VendorID { protected set; get; }
        public override ushort ProductID { protected set; get; }

        public override string Manufacturer { protected set; get; }
        public override string ProductName { protected set; get; }
        public override string SerialNumber { protected set; get; }
        public override uint ConfigurationCount { protected set; get; }

        public override string GetIndexedString(byte index)
        {
            throw new global::System.NotImplementedException();
        }

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