using System;
using System.Runtime.InteropServices;

namespace Native.Windows
{
    public static partial class Windows
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVPROPKEY
        {
            public Guid fmtid;
            public uint pid;
        }

        private static readonly Guid DevicePropertyGuid = new Guid("{0xa45c254e, 0xdf1c, 0x4efd, {0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0}}");
        private static readonly Guid DeviceInterfacePropertyGuid = new Guid("{0x026e516e, 0xb814, 0x414b, {0x83, 0xcd, 0x85, 0x6d, 0x6f, 0xef, 0x48, 0x22}}");

        public static DEVPROPKEY DEVPKEY_Device_DeviceDesc => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 2
        };

        public static DEVPROPKEY DEVPKEY_Device_ClassGuid => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 10
        };

        public static DEVPROPKEY DEVPKEY_Device_Manufacturer => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 13
        };

        public static DEVPROPKEY DEVPKEY_Device_FriendlyName => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 14
        };

        public static DEVPROPKEY DEVPKEY_Device_LocationInfo => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 15
        };

        public static DEVPROPKEY DEVPKEY_Device_Address => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 30
        };

        public static DEVPROPKEY DEVPKEY_Device_InstanceId => new DEVPROPKEY
        {
            fmtid = new Guid("{0x78c34fc8, 0x104a, 0x4aca, {0x9e, 0xa4, 0x52, 0x4d, 0x52, 0x99, 0x6e, 0x57}}"),
            pid = 256
        };

        public static DEVPROPKEY DEVPKEY_DeviceInterface_FriendlyName => new DEVPROPKEY
        {
            fmtid = DeviceInterfacePropertyGuid,
            pid = 2
        };

        public static DEVPROPKEY DEVPKEY_DeviceInterface_ClassGuid => new DEVPROPKEY
        {
            fmtid = DeviceInterfacePropertyGuid,
            pid = 4
        };
    }
}