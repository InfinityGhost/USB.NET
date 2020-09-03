using System;
using System.Runtime.InteropServices;

namespace USB.NET.Descriptors
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EndpointDescriptor
    {
        /// <summary>
        /// Size of this descriptor in bytes
        /// </summary>
        public byte bLength;

        /// <summary>
        /// The current descriptor type
        /// </summary>
        public DescriptorType bDescriptorType;

        /// <summary>
        /// The address of the endpoint on the USB device described by this descriptor.
        /// </summary>
        /// <value>Bitmap</value>
        public byte bEndpointAddress;

        /// <summary>
        /// The endpoint attribute when configured through bConfigurationValue
        /// </summary>
        /// <value>Bitmap</value>
        public byte bmAttributes;

        /// <summary>
        /// The maximum packet size for this endpoint.
        /// </summary>
        public ushort wMaxPacketSize;

        /// <summary>
        /// Interval for polling endpoint for data transfers.
        /// Expressed in frames or micro-frames depending on the operating speed (1m or 125μs)
        /// </summary>
        public byte bInterval;
    }
}