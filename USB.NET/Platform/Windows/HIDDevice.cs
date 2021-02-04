using System.Collections.Generic;
using System.Linq;
using USB.NET.Descriptors;
using USB.NET.Platform.Windows.Enumerators;

namespace USB.NET.Platform.Windows
{
    internal sealed class HIDDevice : Device
    {
        public HIDDevice(string path, Dictionary<int, List<HIDFragment>> hidFragments)
        {
            var fragment = hidFragments.Values.First()[0];

            VendorID = fragment.VendorID;
            ProductID = fragment.ProductID;

            Manufacturer = fragment.Manufacturer;
            ProductName = fragment.ProductName;
            SerialNumber = fragment.SerialNumber;

            InternalFilePath = path;
        }

        private Dictionary<int, List<HIDFragment>> HIDFragments;

        public override void ClearFeature(ushort feature)
        {
            throw new System.NotImplementedException();
        }

        public override Configuration GetConfiguration()
        {
            throw new System.NotImplementedException();
        }

        public override DeviceDescriptor GetDeviceDescriptor()
        {
            throw new System.NotImplementedException();
        }

        public override string GetIndexedString(byte index)
        {
            throw new System.NotImplementedException();
        }

        public override bool SetConfiguration(ushort index)
        {
            throw new System.NotImplementedException();
        }

        public override void SetFeature(ushort feature)
        {
            throw new System.NotImplementedException();
        }
    }
}