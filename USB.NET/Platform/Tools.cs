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

        public static string RetrieveString<T>(int bufferSize, Func<StringBuilder, bool> func, string msg, Action<DelayedException<T>> e) where T : Exception, new()
        {
            var strBuffer = new StringBuilder(bufferSize);
            if (!func(strBuffer))
                e(new DelayedException<T>(msg));

            return strBuffer.ToString();
        }
    }
}