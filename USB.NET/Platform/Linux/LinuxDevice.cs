using System.Globalization;
using System.IO;
using Native.Linux.libudev;
using Native.Linux.libc;
using USB.NET.Descriptors;
using USB.NET.Packets;
using System;
using System.Runtime.InteropServices;
using System.Text;
using Native.Linux.Kernel.USB;

namespace USB.NET.Platform.Linux
{
    using static Platform.Tools;
    using static Platform.Linux.Tools;
    using static libudevMethods;
    using static libcMethods;
    
    internal unsafe sealed class LinuxDevice : Device
    {
        internal unsafe LinuxDevice(udev_device* udevDevice, string devPath, DeviceDescriptor descriptor, byte[] otherDescriptors = null)
        {
            InternalFilePath = devPath;
            this.descriptor = descriptor;
            this.otherDescriptors = otherDescriptors;
            SetValues(descriptor);

            var busnum = udev_device_get_sysattr_value(udevDevice, "busnum");
            var devnum = udev_device_get_sysattr_value(udevDevice, "devnum");
            this.devname = $"/dev/bus/usb/{int.Parse(busnum):D3}/{int.Parse(devnum):D3}";
            this.Manufacturer = udev_device_get_sysattr_value(udevDevice, "manufacturer");
            this.ProductName = udev_device_get_sysattr_value(udevDevice, "product");
            this.SerialNumber = udev_device_get_property_value(udevDevice, "serial");
        }

        private DeviceDescriptor descriptor;
        private string devname;
        private byte[] otherDescriptors;

        public override string GetIndexedString(byte index)
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_TYPE_STANDARD | RequestType.USB_RECIP_DEVICE),
                bRequest = (byte)Request.GET_DESCRIPTOR,
                wValue = string_index(0),
                wIndex = 0,
                wLength = 255
            };
            
            fixed (char* strbuf = new char[256])
            {
                setup.data = strbuf;

                var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
                if (fd == -1 || ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                    throw CreateIOExceptionFromLastError();

                setup.wIndex = (ushort)(strbuf[2] | strbuf[3] << 8);
                setup.wValue = string_index(index);

                for (int i = 0; i < 256; i++)
                    ((byte*)setup.data)[i] = 0;

                ioctl(fd, USBDEVFS_CONTROL, ref setup);
                close(fd);
                
                var sb = new StringBuilder();
                var retBuf = (char*)setup.data;
                for (int i = 1; i < 255; i++)
                {
                    var c = retBuf[i];
                    if (c == 0)
                        break;
                    sb.Append(c);
                }
                return sb.ToString();
            }
        }

        public override bool SetConfiguration(ushort index)
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_TYPE_STANDARD | RequestType.USB_RECIP_DEVICE),
                bRequest = (byte)Request.SET_CONFIGURATION,
                wValue = index
            };

            ControlRequest(devname, setup);
            return true;
        }

        public override Configuration GetConfiguration()
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_TYPE_STANDARD | RequestType.USB_RECIP_DEVICE),
                bRequest = (byte)Request.GET_CONFIGURATION,
                wLength = 1
            };
            byte buf = 0;
            setup.data = &buf;
            setup = ControlRequest(devname, setup);

            byte currentConfiguration = ((byte*)setup.data)[0];
            fixed (byte* configDescriptors = otherDescriptors)
            {
                var descriptorPtr = (ConfigurationDescriptor*)configDescriptors;
                ushort currentDataIndex = 0;
                for (int i = 0; i < ConfigurationCount; i++)
                {
                    var descriptor = *descriptorPtr;
                    if (descriptor.bConfigurationValue == currentConfiguration)
                    {
                        var start = currentDataIndex + sizeof(ConfigurationDescriptor);
                        var end = currentDataIndex + descriptor.wTotalLength - sizeof(ConfigurationDescriptor) - 1;
                        return new LinuxDeviceConfiguration(descriptor, devname, otherDescriptors[start..end]);
                    }
                    else
                    {
                        descriptorPtr = (ConfigurationDescriptor*)((byte*)descriptorPtr + descriptor.wTotalLength);
                        currentDataIndex += descriptor.wTotalLength;
                    }
                }
            }
            throw new Exception("Current configuration not found in cached descriptors.");
        }

        public override DeviceDescriptor GetDeviceDescriptor()
        {
            return this.descriptor;
        }

        public override void SetFeature(ushort feature)
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_RECIP_DEVICE | RequestType.USB_TYPE_STANDARD),
                bRequest = (byte)Request.SET_FEATURE,
                wIndex = 0,
                wValue = feature
            };

            ControlRequest(devname, setup);
        }

        public override void ClearFeature(ushort feature)
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_RECIP_DEVICE | RequestType.USB_TYPE_STANDARD),
                bRequest = (byte)Request.CLEAR_FEATURE,
                wIndex = 0,
                wValue = feature
            };

            ControlRequest(devname, setup);
        }
    }
}