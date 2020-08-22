using System;
using System.Collections.Generic;

namespace USB.NET
{
    public abstract class DeviceManager
    {
        public event Action<Device> DeviceConnected;

        public abstract IEnumerable<Device> GetAllDevices();

        protected virtual void OnDeviceConnected(Device device)
        {
            DeviceConnected?.Invoke(device);
        }
    }
}