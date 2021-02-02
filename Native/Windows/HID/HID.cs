using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Native.Windows
{
    public static partial class HID
    {
        [DllImport("hid.dll", EntryPoint = "HidD_GetHidGuid", SetLastError = true)]
        public static extern void GetHidDGuid(out Guid Guid);

        [DllImport("hid.dll", EntryPoint = "HidD_GetAttributes", SetLastError = true)]
        public static extern bool GetAttributes(IntPtr HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

        [DllImport("hid.dll", EntryPoint = "HidD_GetPreparsedData", SetLastError = true)]
        public static extern bool GetPreparsedData(IntPtr HidDeviceObject, out IntPtr PreparsedData);

        [DllImport("hid.dll", EntryPoint = "HidP_GetCaps", SetLastError = true)]
        public static extern int GetCaps(IntPtr PreparsedData, out HIDP_CAPS Capabilities);

        [DllImport("hid.dll", EntryPoint = "HidD_GetManufacturerString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetManufacturerString(IntPtr HidDeviceObject, StringBuilder Buffer, int BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_GetProductString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetProductString(IntPtr HidDeviceObject, StringBuilder Buffer, int BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_GetSerialNumberString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetSerialNumberString(IntPtr HidDeviceObject, StringBuilder Buffer, int BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_FreePreparsedData", SetLastError = true)]
        public static extern bool FreePreparsedData(IntPtr PreparsedData);
    }
}