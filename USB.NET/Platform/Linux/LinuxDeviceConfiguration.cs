using System;
using Native.Linux.Kernel.USB;
using Native.Linux.libc;
using USB.NET.Descriptors;
using USB.NET.Packets;

namespace USB.NET.Platform.Linux
{
    using static Linux.Tools;
    using static libcMethods;
    
    internal unsafe sealed class LinuxDeviceConfiguration : Configuration
    {
        internal LinuxDeviceConfiguration(ConfigurationDescriptor descriptor, string devname, byte[] otherDescriptors = null)
        {
            this.devname = devname;
            this.descriptor = descriptor;
            this.otherDescriptors = otherDescriptors;
            SetValues(descriptor);
        }

        private ConfigurationDescriptor descriptor;
        private string devname;
        private byte[] otherDescriptors;

        public override bool SetInterface(ushort index)
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_RECIP_DEVICE | RequestType.USB_TYPE_STANDARD),
                bRequest = (byte)Request.SET_INTERFACE,
                wValue = index
            };
            ControlRequest(devname, setup);
            return true;
        }

        public override Interface GetInterface()
        {
            var setup = new usbfs_ctrltransfer
            {
                bRequestType = (byte)(RequestType.USB_DIR_IN | RequestType.USB_RECIP_DEVICE | RequestType.USB_TYPE_STANDARD),
                bRequest = (byte)Request.GET_INTERFACE,
                wValue = 0,
                wIndex = 0,
                wLength = 1
            };
            byte buf = 0;
            setup.data = &buf;
            var fd = open(devname, oflag.NONBLOCK | oflag.RDWR);
            if (fd != -1)
            {
                ioctl(fd, USBDEVFS_CONTROL, ref setup);
                close(fd);
            }
            else
            {
                throw CreateNativeException();
            }

            byte currentInterface = ((byte*)setup.data)[0];
            fixed (byte* interfaceDescriptors = otherDescriptors)
            {
                var descriptorPtr = (InterfaceDescriptor*)interfaceDescriptors;

                int currentIndex = 0;
                while (currentIndex < otherDescriptors.Length)
                {
                    var descriptor = *descriptorPtr;
                    if (descriptor.bDescriptorType.HasFlag(DescriptorType.Interface))
                    {
                        if (descriptor.bInterfaceNumber == currentInterface)
                        {
                            var end = currentIndex + sizeof(InterfaceDescriptor);
                            return new LinuxDeviceInterface(descriptor, devname, otherDescriptors[currentIndex..^0]);
                        }
                    }
                    currentIndex += (descriptor.bLength);
                    descriptorPtr = (InterfaceDescriptor*)(interfaceDescriptors + currentIndex);
                }
            }
            throw new Exception("Current interface not found in cached descriptors.");
        }

        public override ConfigurationDescriptor GetConfigurationDescriptor()
        {
            return this.descriptor;
        }
    }
}