using System;

namespace Native.Windows
{
    public static partial class Windows
    {
        public static void ExecuteWithRet<T>(Func<T> func, Action<T> retFunc)
        {
            retFunc(func());
        }

        public static bool ExecuteWithRet<T>(Func<T> func, Func<T, bool> retFunc)
        {
            return retFunc(func());
        }

        public static readonly IntPtr InvalidHandle = new IntPtr(-1);
        public static Guid GUID_USB_BUS => new Guid("{36fc9e60-c465-11cf-8056-444553540000}");
        public static Guid GUID_USB_HID => new Guid("{745a17a0-74d3-11d0-b6fe-00a0c90f57da}");
        public static Guid GUID_DEVINTERFACE_USB_HOST_CONTROLLER => new Guid("{3ABF6F2D-71C4-462A-8A92-1E6861E6AF27}");
        public static Guid GUID_DEVINTERFACE_USB_HUB => new Guid("{F18A0E88-C30C-11D0-8815-00A0C906BED8}");
        public static Guid GUID_DEVINTERFACE_USB_DEVICE => new Guid("{A5DCBF10-6530-11D2-901F-00C04FB951ED}");
        public static Guid GUID_DEVINTERFACE_HID => new Guid("{4D1E55B2-F16F-11CF-88CB-001111000030}");
    }
}