using System;

namespace USB.NET.Descriptors
{
    public class DeviceDescriptor : Descriptor
    {
        public DeviceDescriptor()
        {
            Raw = new byte[Size];
            bLength = Size;
            bDescriptorType = DescriptorType.Device;
        }

        private DeviceDescriptor(byte[] value) => Raw = value;

        private const byte Size = 18;

        /// <summary>
        /// USB specification release number in binary-coded decimal
        /// </summary>
        public ushort bcdUSB
        {
            set => SetValue(value, 2);
            get => BitConverter.ToUInt16(Raw[2..3]);
        }

        /// <summary>
        /// Class code assigned by the USB-IF
        /// </summary>
        public DeviceClass bDeviceClass
        {
            set => Raw[4] = (byte)value;
            get => (DeviceClass)Raw[4];
        }

        /// <summary>
        /// Subclass code assigned by the USB-IF
        /// </summary>
        public byte bDeviceSubClass
        {
            set => Raw[5] = value;
            get => Raw[5];
        }

        /// <summary>
        /// Protocol code assigned by the USB-IF
        /// </summary>
        public byte bDeviceProtocol
        {
            set => Raw[6] = value;
            get => Raw[6];
        }

        /// <summary>
        /// Maximum packet size for endpoint 0
        /// </summary>
        /// <value>8, 16, 32, 64</value>
        public byte bMaxPacketSize0
        {
            set => Raw[7] = value;
            get => Raw[7];
        }

        /// <summary>
        /// Vendor ID assigned by the USB-IF
        /// </summary>
        public ushort idVendor
        {
            set => SetValue(value, 8);
            get => BitConverter.ToUInt16(Raw[8..9]);
        }

        /// <summary>
        /// Product ID assigned by the manufacturer
        /// </summary>
        public ushort idProduct
        {
            set => SetValue(value, 10);
            get => BitConverter.ToUInt16(Raw[10..11]);
        }

        /// <summary>
        /// Device release number in binary coded decimal
        /// </summary>
        public ushort bcdDevice
        {
            set => SetValue(value, 12);
            get => BitConverter.ToUInt16(Raw[12..13]);
        }

        /// <summary>
        /// Index of the string descriptor describing the manufacturer
        /// </summary>
        public byte iManufacturer
        {
            set => Raw[14] = value;
            get => Raw[14];
        }

        /// <summary>
        /// Index of the string descriptor describing the product
        /// </summary>
        public byte iProduct
        {
            set => Raw[15] = value;
            get => Raw[15];
        }

        /// <summary>
        /// Index of the string descriptor describing the serial number
        /// </summary>
        public byte iSerialNumber
        {
            set => Raw[16] = value;
            get => Raw[16];
        }

        /// <summary>
        /// Index of the string descriptor containing the total amount of configurations
        /// </summary>
        public byte iNumConfigurations
        {
            set => Raw[17] = value;
            get => Raw[17];
        }

        public static implicit operator byte[](DeviceDescriptor thisDescriptor)
        {
            return (byte[])thisDescriptor.Raw.Clone();
        }

        public static implicit operator DeviceDescriptor(byte[] raw)
        {
            return raw[0] == 18 && raw[1] == (byte)DescriptorType.Device ? new DeviceDescriptor(raw) : throw new InvalidCastException();
        }
    }
}