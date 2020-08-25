using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Device : IFeature
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

        public abstract void SetConfiguration(ushort index);
        public abstract Configuration GetConfiguration();

        public abstract void SetFeature(ushort feature);
        public abstract void ClearFeature(ushort feature);

        public abstract string GetIndexedString(byte index);
        
        public abstract DeviceDescriptor GetDeviceDescriptor();
    }
}