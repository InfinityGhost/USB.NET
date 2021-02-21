using System.Runtime.InteropServices;

namespace Native.Windows
{
    public static partial class HID
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct HIDD_ATTRIBUTES
        {
            public int Size;
            public short VendorID;
            public short ProductID;
            public short VersionNumber;
        }
    }
}