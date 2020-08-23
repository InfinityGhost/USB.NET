using System.Globalization;
using System.IO;
using Native.Linux.libudev;
using USB.NET.Descriptors;

namespace USB.NET.Platform.Linux
{
    using static libudevMethods;
    
    public sealed class LinuxDevice : Device
    {
        internal unsafe LinuxDevice(udev_device* udevDevice, string devPath, DeviceDescriptor descriptor)
        {
            InternalFilePath = devPath;
            SetValues(descriptor);

            Manufacturer = udev_device_get_sysattr_value(udevDevice, "manufacturer");
            ProductName = udev_device_get_sysattr_value(udevDevice, "product");
            SerialNumber = udev_device_get_property_value(udevDevice, "serial");
        }

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