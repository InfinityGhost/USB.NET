using System;

namespace USB.NET.Descriptors
{
    public class EndpointDescriptor : Descriptor
    {
        public EndpointDescriptor()
        {
            Raw = new byte[Size];
            bLength = Size;
            bDescriptorType = DescriptorType.Endpoint;
        }

        protected EndpointDescriptor(byte[] value) => Raw = value;
        
        private const byte Size = 7;

        /// <summary>
        /// The address of the endpoint on the USB device described by this descriptor.
        /// </summary>
        /// <value>Bitmap</value>
        public byte bEndpointAddress
        {
            set => Raw[2] = value;
            get => Raw[2];
        }

        /// <summary>
        /// The endpoint attribute when configured through bConfigurationValue
        /// </summary>
        /// <value>Bitmap</value>
        public byte bmAttributes
        {
            set => Raw[3] = value;
            get => Raw[3];
        }

        /// <summary>
        /// The maximum packet size for this endpoint.
        /// </summary>
        public ushort wMaxPacketSize
        {
            set => SetValue(value, 4);
            get => BitConverter.ToUInt16(Raw[4..5]);
        }

        /// <summary>
        /// Interval for polling endpoint for data transfers.
        /// Expressed in frames or micro-frames depending on the operating speed (1m or 125μs)
        /// </summary>
        public byte bInterval
        {
            set => Raw[6] = value;
            get => Raw[6];
        }
    }
}