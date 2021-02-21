using System;

namespace Native.Windows
{
    public static partial class SetupAPI
    {
        [Flags]
        public enum DIGCF
        {
            None = 0,
            Default = 1,
            Present = 2,
            AllClasses = 4,
            Profile = 8,
            DeviceInterface = 16
        }
    }
}