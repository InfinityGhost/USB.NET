using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace USB.NET.Tests
{
    using static Trace;

    [TestClass]
    public class HostTests
    {
        [TestMethod]
        public void GetAllDevicesTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                WriteLine(device.ProductName, "ProductName");
                WriteLine(device.Manufacturer, "Manufacturer");
                WriteLine($"{device.VendorID:X4}:{device.ProductID:X4}", "VendorID:ProductID");
                WriteLine(device.InternalFilePath, "InternalFilePath");
                WriteLine(string.Empty);
            }
        }
    }
}
