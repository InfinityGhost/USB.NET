using System;
using System.IO;
using System.Text;
using USB.NET.Descriptors;
using static Native.Windows.HID;
using static Native.Windows.Kernel32;

namespace USB.NET.Platform.Windows
{
    public class WindowsDevice : Device
    {
        public WindowsDevice(IntPtr handle, string path, HIDD_ATTRIBUTES attributes, HIDP_CAPS capabilities)
        {
            InternalFilePath = path;
            VendorID = (ushort)attributes.VendorID;
            ProductID = (ushort)attributes.ProductID;

            var strBuffer = new StringBuilder(256);

            Manufacturer = Retrieve(handle, GetManufacturerString);
            ProductName = Retrieve(handle, GetProductString);
            SerialNumber = Retrieve(handle, GetSerialNumberString);

            CloseHandle(handle);
        }

        public string Location { init; get; }

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
            var deviceHandle = CreateFile(
                InternalFilePath,
                FileAccess.ReadWrite,
                FileShare.ReadWrite,
                IntPtr.Zero,
                FileMode.Open,
                FileAttributes.Device,
                IntPtr.Zero
            );

            throw new System.NotImplementedException();
        }

        private delegate bool DeviceStringGetter(IntPtr handle, StringBuilder buffer, int size);

        private string Retrieve(IntPtr handle, DeviceStringGetter func)
        {
            var strBuffer = new StringBuilder(256);

            if (func(handle, strBuffer, 256))
                return strBuffer.ToString();
            else
                return "";
        }
    }
}