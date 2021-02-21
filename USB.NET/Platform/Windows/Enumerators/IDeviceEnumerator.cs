
using System;
using System.Collections.Generic;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal interface IDeviceEnumerator : IDisposable
    {
        IEnumerable<Device> GetDevices();
    }
}