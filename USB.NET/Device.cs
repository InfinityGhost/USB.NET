namespace USB.NET
{
    public abstract class Device
    {
        public abstract ushort VendorID { get; }
        public abstract ushort ProductID { get; }

        public abstract string Manufacturer { get; }
        public abstract string ProductName { get; }
        public abstract string SerialNumber { get; }
        public abstract string GetIndexedString(byte index);

        public abstract uint ConfigurationCount { get; }
        public abstract void SetConfiguration(uint index, bool enabled);
        public abstract Configuration GetConfiguration(uint index);
    }
}