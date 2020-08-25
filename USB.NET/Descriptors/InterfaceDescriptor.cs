namespace USB.NET.Descriptors
{
    public struct InterfaceDescriptor
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
        /// The number of this interface
        /// </summary>
        public byte bInterfaceNumber;

        /// <summary>
        /// Value used to select an alternate setting for the interface identified in the prior field.
        /// Allows an interface to change the settings on the fly
        /// </summary>
        public byte bAlternateSetting;

        /// <summary>
        /// Number of endpoints used by this interface (excluding endpoint 0)
        /// </summary>
        public byte bNumEndpoints;

        /// <summary>
        /// The class code assigned by the USB-IF
        /// </summary>
        public byte bInterfaceClass;

        /// <summary>
        /// The subclass code assigned by the USB-IF
        /// </summary>
        public byte bInterfaceSubClass;

        /// <summary>
        /// The protocol code assigned by the USB-IF
        /// </summary>
        public byte bInterfaceProtocol;

        /// <summary>
        /// The index of the string descriptor describing this interface
        /// </summary>
        public byte iInterface;
    }
}