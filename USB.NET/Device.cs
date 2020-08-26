using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Device : IFeature
    {
        public virtual ushort VendorID { protected set; get; }
        public virtual ushort ProductID { protected set; get; }

        public virtual string Manufacturer { protected set; get; }
        public virtual string ProductName { protected set; get; }
        public virtual string SerialNumber { protected set; get; }
        public virtual uint ConfigurationCount { protected set; get; }
        public virtual string InternalFilePath { protected set; get; }

        protected virtual void SetValues(DeviceDescriptor descriptor)
        {
            VendorID = descriptor.idVendor;
            ProductID = descriptor.idProduct;
            ConfigurationCount = descriptor.iNumConfigurations;
        }

        public abstract bool SetConfiguration(ushort index);
        public abstract Configuration GetConfiguration();

        public abstract void SetFeature(ushort feature);
        public abstract void ClearFeature(ushort feature);

        public abstract string GetIndexedString(byte index);
        
        public abstract DeviceDescriptor GetDeviceDescriptor();
    }
}