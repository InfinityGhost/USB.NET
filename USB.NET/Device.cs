using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Device
    {
        public abstract ushort VendorID { protected set; get; }
        public abstract ushort ProductID { protected set; get; }

        public abstract string Manufacturer { protected set; get; }
        public abstract string ProductName { protected set; get; }
        public abstract string SerialNumber { protected set; get; }
        public abstract uint ConfigurationCount { protected set; get; }
        public virtual string InternalFilePath { protected set; get; }

        protected virtual void SetValues(DeviceDescriptor descriptor)
        {
            VendorID = descriptor.idVendor;
            ProductID = descriptor.idProduct;
            ConfigurationCount = descriptor.iNumConfigurations;
        }

        public abstract void SetConfiguration(uint index, bool enabled);
        public abstract Configuration GetConfiguration(uint index);
        public abstract string GetIndexedString(byte index);
    }
}