using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Native.Linux.libudev;
using USB.NET.Platform.Linux.Exceptions;

namespace USB.NET.Platform.Linux
{
    using static libudevMethods;
    
    public unsafe sealed class LinuxDeviceManager : DeviceManager, IDisposable
    {
        public LinuxDeviceManager()
        {
        }

        private udev* udev = udev_new();

        public override IEnumerable<Device> GetAllDevices()
        {
            var abstractedDevices = new Collection<LinuxDevice>();

            var udevEnumerator = udev_enumerate_new(udev);
            if (udevEnumerator == null)
                throw new UdevException("Failed to create udev enumerator.");
            
            udev_enumerate_add_match_subsystem(udevEnumerator, "usb");
            udev_enumerate_add_match_property(udevEnumerator, "DEVTYPE", "usb_device");
            udev_enumerate_scan_devices(udevEnumerator);

            bool tryIterate(udev_list_entry* entry, out udev_list_entry* next)
            {
                next = udev_list_entry_get_next(entry);
                return next != null;
            }
            var deviceList = udev_enumerate_get_list_entry(udevEnumerator);

            udev_list_entry* entry = deviceList;
            while (tryIterate(entry, out entry))
            {
                string path = udev_list_entry_get_name(entry);
                var dev = new LinuxDevice(udev, path);
                abstractedDevices.Add(dev);
            }
            
            return abstractedDevices;
        }

        public void Dispose()
        {
            udev_unref(udev);
            GC.SuppressFinalize(this);
        }
    }
}