using System.Runtime.InteropServices;

namespace USB.NET.Descriptors
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ConfigurationDescriptor
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
        /// Total length of data returned for this configuration. Includes the length of all descriptors returned for this configuration.
        /// </summary>
        public ushort wTotalLength;

        /// <summary>
        /// Number of interfaces supported by this configuration
        /// </summary>
        public byte bNumInterfaces;

        /// <summary>
        /// Value to select this configuration with SetConfiguration()
        /// </summary>
        public byte bConfigurationValue;

        /// <summary>
        /// Index of the string descriptor describing this configuration
        /// </summary>
        public byte iConfiguration;

        /// <summary>
        /// Bitmap describing the configuration's power attributes
        /// </summary>
        public byte bmAttributes;

        /// <summary>
        /// Maximum power consumption of the USB device from the bus in this specific configuration when the device is fully operational.
        /// </summary>
        /// <value>2mA units</value>
        public byte bMaxPower;
    }
}