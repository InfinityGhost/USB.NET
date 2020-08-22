namespace USB.NET.Descriptors
{
    public class InterfaceDescriptor : Descriptor
    {
        public InterfaceDescriptor()
        {
            Raw = new byte[Size];
            bLength = Size;
            bDescriptorType = DescriptorType.Interface;
        }

        protected InterfaceDescriptor(byte[] value) => Raw = value;

        private const byte Size = 9;

        /// <summary>
        /// The number of this interface
        /// </summary>
        public byte bInterfaceNumber
        {
            set => Raw[2] = value;
            get => Raw[2];
        }

        /// <summary>
        /// Value used to select an alternate setting for the interface identified in the prior field.
        /// Allows an interface to change the settings on the fly
        /// </summary>
        public byte bAlternateSetting
        {
            set => Raw[3] = value;
            get => Raw[3];
        }

        /// <summary>
        /// Number of endpoints used by this interface (excluding endpoint 0)
        /// </summary>
        public byte bNumEndpoints
        {
            set => Raw[4] = value;
            get => Raw[4];
        }

        /// <summary>
        /// The class code assigned by the USB-IF
        /// </summary>
        public byte bInterfaceClass
        {
            set => Raw[5] = value;
            get => Raw[5];
        }

        /// <summary>
        /// The subclass code assigned by the USB-IF
        /// </summary>
        public byte bInterfaceSubClass
        {
            set => Raw[6] = value;
            get => Raw[6];
        }

        /// <summary>
        /// The protocol code assigned by the USB-IF
        /// </summary>
        public byte bInterfaceProtocol
        {
            set => Raw[7] = value;
            get => Raw[7];
        }

        /// <summary>
        /// The index of the string descriptor describing this interface
        /// </summary>
        public byte iInterface
        {
            set => Raw[8] = value;
            get => Raw[8];
        }
    }
}