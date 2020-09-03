using Native.Linux.Kernel.USB;
using Native.Linux.libc;
using Native.Linux.libudev;
using USB.NET.Descriptors;
using USB.NET.Packets;
using USB.NET.Platform.Linux.Exceptions;
using System.Collections.Generic;
using System;

namespace USB.NET.Platform.Linux
{
    using static libcMethods;
    using static libudevMethods;
    
    internal unsafe class LinuxDeviceInterface : Interface
    {
        internal LinuxDeviceInterface(InterfaceDescriptor descriptor, string devname, byte[] otherDescriptors = null)
        {
            this.descriptor = descriptor;
            this.devname = devname;
            this.otherDescriptors = otherDescriptors;
            SetValues(descriptor);
        }

        private InterfaceDescriptor descriptor;
        private string devname;
        private byte[] otherDescriptors;

        public override Endpoint GetEndpoint(uint index)
        {
            var udev = udev_new();
            var udevEnumerator = udev_enumerate_new(udev);
            if (udevEnumerator == null)
                throw new UdevException("Failed to create udev enumerator.");

            udev_enumerate_add_match_subsystem(udevEnumerator, "hidraw");
            udev_enumerate_scan_devices(udevEnumerator);

            List<string> endpointPaths = new List<string>();
            for (var entry = udev_enumerate_get_list_entry(udevEnumerator); entry != null; entry = udev_list_entry_get_next(entry))
            {
                var sysPath = udev_list_entry_get_name(entry);
                udev_device* hidEndpoint = udev_device_new_from_syspath(udev, sysPath);
                udev_device* parentDevice = udev_device_get_parent_with_subsystem_devtype(hidEndpoint, "usb", "usb_device");
                var parentDevname = udev_device_get_property_value(parentDevice, "DEVNAME");
                if (parentDevname == devname)
                    endpointPaths.Add(sysPath);
            }

            throw new NotImplementedException();
        }

        public override InterfaceDescriptor GetDescriptor()
        {
            return this.descriptor;
        }
    }
}