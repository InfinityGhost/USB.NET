using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Native.Linux.libudev;
using USB.NET.Descriptors;
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

            // Iterate through all USB class devices
            for (var entry = udev_enumerate_get_list_entry(udevEnumerator); entry != null; entry = udev_list_entry_get_next(entry))
            {
                // Get the USB device in udev
                string path = udev_list_entry_get_name(entry);
                var udevDevice = udev_device_new_from_syspath(udev, path);
                
                // Grab the device descriptor for parsing
                var rawDescriptorPath = udev_device_get_property_value(udevDevice, "DEVNAME");
                fixed (void* readbuf = File.ReadAllBytes(rawDescriptorPath)[0..18])
                {
                    var deviceDescriptor = *(DeviceDescriptor*)readbuf;
                
                    // Filter out USB hubs, USB controllers, etc. 
                    if (deviceDescriptor.bDeviceClass == DeviceClass.Hub)
                        continue;

                    var dev = new LinuxDevice(udevDevice, path, deviceDescriptor);
                    abstractedDevices.Add(dev);
                }
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