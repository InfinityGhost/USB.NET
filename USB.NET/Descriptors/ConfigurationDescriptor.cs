using System;

namespace USB.NET.Descriptors
{
    public class ConfigurationDescriptor : Descriptor
    {
        public ConfigurationDescriptor()
        {
            Raw = new byte[Size];
            bLength = Size;
            bDescriptorType = DescriptorType.Configuration;
        }

        protected ConfigurationDescriptor(byte[] value) => Raw = value;

        private const byte Size = 9;

        /// <summary>
        /// Total length of data returned for this configuration. Includes the length of all descriptors returned for this configuration.
        /// </summary>
        public ushort wTotalLength
        {
            set => SetValue(value, 2);
            get => BitConverter.ToUInt16(Raw[2..3]);
        }

        /// <summary>
        /// Number of interfaces supported by this configuration
        /// </summary>
        public byte bNumInterfaces
        {
            set => Raw[4] = value;
            get => Raw[4];
        }

        /// <summary>
        /// Value to select this configuration with SetConfiguration()
        /// </summary>
        public byte bConfigurationValue
        {
            set => Raw[5] = value;
            get => Raw[5];
        }

        /// <summary>
        /// Index of the string descriptor describing this configuration
        /// </summary>
        public byte iConfiguration
        {
            set => Raw[6] = value;
            get => Raw[6];
        }

        /// <summary>
        /// Bitmap describing the configuration's power attributes
        /// </summary>
        public byte bmAttributes
        {
            set => Raw[7] = value;
            get => Raw[7];
        }

        /// <summary>
        /// Maximum power consumption of the USB device from the bus in this specific configuration when the device is fully operational.
        /// </summary>
        /// <value>2mA units</value>
        public byte bMaxPower
        {
            set => Raw[8] = value;
            get => Raw[8];
        }

        public static implicit operator ConfigurationDescriptor(byte[] raw)
        {
            return raw[1] == (byte)DescriptorType.Configuration ?
                new ConfigurationDescriptor(raw) : throw new InvalidCastException();
        }
    }
}