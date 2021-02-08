using System;
using System.Text;
using Native;
using USB.NET.Descriptors;

namespace USB.NET.Platform
{
    internal static class Tools
    {
        public static ushort string_index(byte index)
        {
            return (ushort)(((byte)DescriptorType.String << 8) | index);
        }

        public static T RetrieveString<T>(int bufferSize, out string retrievedString, Func<StringBuilder, T> func)
        {
            var strBuffer = new StringBuilder(bufferSize);
            var ret = func(strBuffer);
            retrievedString = strBuffer.ToString();
            return ret;
        }

        public static string RetrieveString(int bufferSize, Action<StringBuilder> func)
        {
            var strBuffer = new StringBuilder(bufferSize);
            func(strBuffer);
            return strBuffer.ToString();
        }
    }
}