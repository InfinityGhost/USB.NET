using System;

namespace USB.NET.Descriptors
{
    public abstract class Descriptor
    {
        public byte[] Raw { protected set; get; }

        /// <summary>
        /// Size of this descriptor in bytes
        /// </summary>
        /// <value></value>
        public virtual byte bLength
        {
            protected set => Raw[0] = value;
            get => Raw[0];
        }

        /// <summary>
        /// The current descriptor type
        /// </summary>
        public DescriptorType bDescriptorType
        {
            protected set => Raw[1] = (byte)value;
            get => (DescriptorType)Raw[1];
        }

        protected void SetValue<T>(T value, int index) where T : struct
        {
            var bytes = value switch
            {
                short shortVal   => BitConverter.GetBytes(shortVal),
                ushort ushortVal => BitConverter.GetBytes(ushortVal),
                int intVal       => BitConverter.GetBytes(intVal),
                uint uintVal     => BitConverter.GetBytes(uintVal),
                long longVal     => BitConverter.GetBytes(longVal),
                ulong ulongVal   => BitConverter.GetBytes(ulongVal),
                _                => throw new InvalidCastException() 
            };
            Buffer.BlockCopy(bytes, 0, Raw, index, bytes.Length);
        }
    }
}