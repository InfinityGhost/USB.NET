using System;
using System.Collections.Generic;
using System.Linq;
using USB.NET.Platform.Windows.Enumerators;

namespace USB.NET.Platform.Windows
{
    public unsafe sealed class WindowsDeviceManager : DeviceManager, IDisposable
    {
        public override IEnumerable<Device> GetAllDevices()
        {
            using var hidEnumerator = new HIDDeviceEnumerator();
            using var winUsbEnumerator = new WinUsbDeviceEnumerator();
            return hidEnumerator.GetDevices().Concat(winUsbEnumerator.GetDevices());
        }

        public void Dispose()
        {
            
        }
    }
}