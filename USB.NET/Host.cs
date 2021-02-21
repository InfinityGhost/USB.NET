using System;
using System.Runtime.InteropServices;
using USB.NET.Platform.Linux;
using USB.NET.Platform.Windows;

namespace USB.NET
{
    public static class Host
    {
        internal enum RuntimePlatform
        {
            Linux = 1,
            Windows = 2,
            MacOS = 3,
        }

        internal static RuntimePlatform CurrentPlatform
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return RuntimePlatform.Windows;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return RuntimePlatform.Linux;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return RuntimePlatform.MacOS;
                else
                    throw new PlatformNotSupportedException();
            }
        }

        public static DeviceManager DeviceManager => deviceManager.Value;

        private static Lazy<DeviceManager> deviceManager = new Lazy<DeviceManager>(() => 
            CurrentPlatform switch
            {
                RuntimePlatform.Windows => new WindowsDeviceManager(),
                RuntimePlatform.Linux => new LinuxDeviceManager(),
                _                       => throw new NotImplementedException()
            });
    }
}