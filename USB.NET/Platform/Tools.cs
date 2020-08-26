using USB.NET.Descriptors;

namespace USB.NET.Platform
{
    internal unsafe static class Tools
    {
        public static ushort string_index(byte index)
        {
            return (ushort)(((byte)DescriptorType.String << 8) | index);
        }
    }
}