using System;
using System.Text;
using static Native.Windows.HID;
using static Native.Windows.Kernel32;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal class HIDFragment
    {
        public HIDFragment(IntPtr handle, HIDD_ATTRIBUTES attributes, HIDP_CAPS caps)
        {
            VendorID = (ushort)attributes.VendorID;
            ProductID = (ushort)attributes.ProductID;

            Manufacturer = Retrieve(handle, GetManufacturerString);
            ProductName = Retrieve(handle, GetProductString);
            SerialNumber = Retrieve(handle, GetSerialNumberString);

            Caps = caps;

            CloseHandle(handle);
        }

        public string USBPath { init; get; }
        public string HIDPath { init; get; }
        public ushort VendorID { get; }
        public ushort ProductID { get; }

        public string Manufacturer { get; }
        public string ProductName { get; }
        public string SerialNumber { get; }

        public HIDP_CAPS Caps { get; }

        private delegate bool DeviceStringGetter(IntPtr handle, StringBuilder buffer, int size);

        private static string Retrieve(IntPtr handle, DeviceStringGetter func)
        {
            var strBuffer = new StringBuilder(256);

            if (func(handle, strBuffer, 256))
                return strBuffer.ToString();
            else
                return "";
        }
    }
}