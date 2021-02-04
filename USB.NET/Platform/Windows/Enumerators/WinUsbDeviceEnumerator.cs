using System;
using System.Collections.Generic;
using System.Linq;
using static Native.Windows.SetupAPI;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal class WinUsbDeviceEnumerator : WindowsDeviceCommonEnumerator
    {
        public WinUsbDeviceEnumerator()
        {
            Initialize(DIGCF.DeviceInterface | DIGCF.Present, HuionWinUsb);
        }

        private Guid HuionWinUsb = new Guid("{62F12D4C-3431-4EFD-8DD7-8E9AAB18D30C}");
        protected override bool ManyOutput => false;

        protected override Device ProcessDevice(SP_DEVINFO_DATA device)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Device> ProcessDeviceManyOutput(SP_DEVINFO_DATA device)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Device> Convert(IEnumerable<object> fragments)
        {
            return fragments.Select(f => (Device)f);
        }
    }
}