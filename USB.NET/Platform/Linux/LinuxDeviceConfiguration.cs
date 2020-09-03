using System;
using Native.Linux.Kernel.USB;
using USB.NET.Descriptors;
using USB.NET.Packets;

namespace USB.NET.Platform.Linux
{
    using static Linux.Tools;
    
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
                wLength = 1
            };
            byte buf = 0;
            setup.data = &buf;
            setup = ControlRequest(devname, setup);

            byte currentInterface = ((byte*)setup.data)[0];
            fixed (byte* interfaceDescriptors = otherDescriptors)
            {
                var descriptorPtr = (InterfaceDescriptor*)interfaceDescriptors;

                for (int i = 0; i < InterfaceCount; i++)
                {
                    var descriptor = *descriptorPtr;
                    if (descriptor.bInterfaceNumber == currentInterface)
                    {
                        var start = i * sizeof(InterfaceDescriptor);
                        var end = start + sizeof(InterfaceDescriptor);
                        return new LinuxDeviceInterface(descriptor, devname, otherDescriptors[start..end]);
                    }
                    else
                    {
                        descriptorPtr++;
                    }
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