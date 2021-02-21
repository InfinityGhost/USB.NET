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
            return Enumerators.SelectMany(d => d.GetDevices());
        }

        private readonly IDeviceEnumerator[] Enumerators =
        {
            new UsbEnumerator(),
            // new WinUsbEnumerator()
        };

        public void Dispose()
        {
            foreach (var enumerator in Enumerators)
                enumerator.Dispose();
        }
    }
}