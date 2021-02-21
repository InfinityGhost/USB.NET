using System.Collections.Generic;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal class WinUsbEnumerator : IDeviceEnumerator
    {
        public IEnumerable<Device> GetDevices()
        {
            yield return null;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}