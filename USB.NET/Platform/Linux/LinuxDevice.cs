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
    using static libudevMethods;
    using static libcMethods;
    
    public unsafe sealed class LinuxDevice : Device
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
            var setup = new ControlPacket
            {
                bRequestType = RequestType.USB_DIR_IN,
                bRequest = Request.GET_DESCRIPTOR,
                wValue = string_index(0),
                wIndex = 0,
                wLength = 255
            };
            
            fixed (char* strbuf = new char[256])
            {
                setup.data = strbuf;

                var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
                if (fd == -1 && ioctl(fd, USBDEVFS_CONTROL, setup) != 0)
                {
                    throw CreateIOExceptionFromLastError();
                }

                setup.wIndex = (ushort)(strbuf[2] | strbuf[3] << 8);
                setup.wValue = string_index(index);

                for (int i = 0; i < 256; i++)
                    ((byte*)setup.data)[i] = 0;

                if (ioctl(fd, USBDEVFS_CONTROL, setup) == 0)
                {
                    var sb = new StringBuilder();
                    var retBuf = (char*)setup.data;
                    for (int i = 0; i < 255; i++)
                    {
                        var c = retBuf[i];
                        if (c == 0)
                            break;
                        sb.Append(c);
                    }
                    return sb.ToString();
                }
                else
                {
                    throw CreateIOExceptionFromLastError();
                }
            }
        }

        private static ushort string_index(byte index)
        {
            return (ushort)(((byte)DescriptorType.String << 8) | index);
        }

        private static IOException CreateIOExceptionFromLastError()
        {
            var errno = (Error)Marshal.GetLastWin32Error();
            return new IOException(errno.ToString());
        }

        public override void SetConfiguration(uint index, bool enabled)
        {
            throw new global::System.NotImplementedException();
        }

        public override Configuration GetConfiguration(uint index)
        {
            throw new global::System.NotImplementedException();
        }

        public override DeviceDescriptor GetDeviceDescriptor()
        {
            return this.descriptor;
        }
    }
}