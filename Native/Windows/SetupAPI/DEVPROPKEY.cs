using System;
using System.Runtime.InteropServices;

namespace Native.Windows
{
    public static partial class SetupAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVPROPKEY
        {
            public Guid fmtid;
            public uint pid;
        }

        private static readonly Guid DevicePropertyGuid = new Guid("{0xa45c254e, 0xdf1c, 0x4efd, {0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0}}");

        public static DEVPROPKEY DEVPKEY_Device_DeviceDesc => new DEVPROPKEY
        {
            fmtid = DevicePropertyGuid,
            pid = 2
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
    }
}