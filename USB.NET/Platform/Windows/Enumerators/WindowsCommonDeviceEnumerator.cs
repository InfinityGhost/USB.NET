using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using USB.NET.Platform.Windows.Exceptions;
using static Native.Windows.SetupAPI;
using static Native.Windows.Windows;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal abstract class WindowsDeviceCommonEnumerator : IDisposable
    {
        protected IntPtr DeviceListPtr;
        protected Guid Guid;
        protected DIGCF DeviceClass;

        protected void Initialize(DIGCF deviceClass, Guid guid)
        {
            Guid = guid;
            DeviceClass = deviceClass;

            DeviceListPtr = SetupDiGetClassDevs(ref Guid, IntPtr.Zero, IntPtr.Zero, DeviceClass);
            if (DeviceListPtr == InvalidHandle)
                throw new WindowsNativeException("Failed to retrieve device list for specified class");
        }

        public IEnumerable<Device> GetDevices()
        {
            if (ManyOutput)
            {
                var devices = GetDevicesInternal(d => ProcessDeviceManyOutput(d))
                    .SelectMany(d => d)
                    .Where(d => d != null);
                return Convert(devices).ToArray();
            }
            else
            {
                var devices = GetDevicesInternal(d => ProcessDevice(d))
                    .Where(d => d != null);
                return Convert(devices).ToArray();
            }
        }

        private IEnumerable<T> GetDevicesInternal<T>(Func<SP_DEVINFO_DATA, T> getFunc)
        {
            for (uint i = 0; ; i++)
            {
                var device = new SP_DEVINFO_DATA { cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA)) };

                SetupDiEnumDeviceInfo(DeviceListPtr, i, ref device);
                if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                    break;

                yield return getFunc(device);
            }
        }

        protected abstract object ProcessDevice(SP_DEVINFO_DATA device);
        protected abstract IEnumerable<object> ProcessDeviceManyOutput(SP_DEVINFO_DATA device);
        protected abstract bool ManyOutput { get; }

        public abstract IEnumerable<Device> Convert(IEnumerable<object> fragments);


        public void Dispose()
        {
            SetupDiDestroyDeviceInfoList(DeviceListPtr);
        }
    }
}