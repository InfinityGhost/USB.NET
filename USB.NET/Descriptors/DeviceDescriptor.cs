using System;

namespace USB.NET.Descriptors
{
    public struct DeviceDescriptor
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
        /// USB specification release number in binary-coded decimal
        /// </summary>
        public ushort bcdUSB;

        /// <summary>
        /// Class code assigned by the USB-IF
        /// </summary>
        public DeviceClass bDeviceClass;

        /// <summary>
        /// Subclass code assigned by the USB-IF
        /// </summary>
        public byte bDeviceSubClass;

        /// <summary>
        /// Protocol code assigned by the USB-IF
        /// </summary>
        public byte bDeviceProtocol;

        /// <summary>
        /// Maximum packet size for endpoint 0
        /// </summary>
        /// <value>8, 16, 32, 64</value>
        public byte bMaxPacketSize0;

        /// <summary>
        /// Vendor ID assigned by the USB-IF
        /// </summary>
        public ushort idVendor;

        /// <summary>
        /// Product ID assigned by the manufacturer
        /// </summary>
        public ushort idProduct;

        /// <summary>
        /// Device release number in binary coded decimal
        /// </summary>
        public ushort bcdDevice;

        /// <summary>
        /// Index of the string descriptor describing the manufacturer
        /// </summary>
        public byte iManufacturer;

        /// <summary>
        /// Index of the string descriptor describing the product
        /// </summary>
        public byte iProduct;

        /// <summary>
        /// Index of the string descriptor describing the serial number
        /// </summary>
        public byte iSerialNumber;

        /// <summary>
        /// Index of the string descriptor containing the total amount of configurations
        /// </summary>
        public byte iNumConfigurations;
    }
}