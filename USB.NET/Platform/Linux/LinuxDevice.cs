using System.Globalization;
using System.IO;
using Native.Linux.libudev;
using Native.Linux.libc;
using USB.NET.Descriptors;
using USB.NET.Packets;
using System;
using System.Runtime.InteropServices;
using System.Text;
using Native.Linux.Linux.USB;

namespace USB.NET.Platform.Linux
{
    using static Tools;
    using static libudevMethods;
    using static libcMethods;
    
    internal unsafe sealed class LinuxDevice : Device
    {
        internal unsafe LinuxDevice(udev_device* udevDevice, string devPath, DeviceDescriptor descriptor)
        {
            InternalFilePath = devPath;
            this.descriptor = descriptor;
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

        public override ushort VendorID { protected set; get; }
        public override ushort ProductID { protected set; get; }

        public override string Manufacturer { protected set; get; }
        public override string ProductName { protected set; get; }
        public override string SerialNumber { protected set; get; }
        public override uint ConfigurationCount { protected set; get; }

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

        public override void SetConfiguration(ushort index)
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_TYPE_STANDARD | RequestType.USB_RECIP_DEVICE),
                bRequest = (byte)Request.SET_CONFIGURATION,
                wValue = (ushort)index
            };

            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd == -1 || ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                throw CreateIOExceptionFromLastError();
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
            
            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd == -1 || ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                throw CreateIOExceptionFromLastError();

            byte currentConfiguration = ((byte*)setup.data)[0];

            setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_RECIP_DEVICE | RequestType.USB_TYPE_STANDARD),
                bRequest = (byte)Request.GET_DESCRIPTOR,
                wValue = (ushort)((byte)DescriptorType.Configuration | currentConfiguration),
            };
            var descriptor = new ConfigurationDescriptor();
            setup.data = &descriptor;

            if (ioctl(fd, USBDEVFS_CONTROL, ref setup) != -1)
            {
                descriptor = *(ConfigurationDescriptor*)setup.data;
                return new LinuxDeviceConfiguration(descriptor, devname);
            }
            else
                throw CreateIOExceptionFromLastError();
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

            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd != -1 && ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                throw CreateIOExceptionFromLastError();
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

            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd != -1 && ioctl(fd, USBDEVFS_CONTROL, ref setup) == -1)
                throw CreateIOExceptionFromLastError();
        }
    }
}