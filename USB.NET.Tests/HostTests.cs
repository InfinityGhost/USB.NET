using System.Diagnostics;
using System.Linq;
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

        const ushort vid = 0x056A;
        const ushort pid = 0x030E;

        [DataTestMethod]
        [DataRow(vid, pid)]
        public void GetDeviceStringTest(ushort vendorId, ushort productId)
        {
            var matching = from device in Host.DeviceManager.GetAllDevices()
                where device.VendorID == vendorId
                where device.ProductID == productId
                select device;
            
            if (matching.Count() == 1)
            {
                var device = matching.FirstOrDefault();
                
                WriteLine(device.ProductName, "ProductName");
                WriteLine(device.Manufacturer, "Manufacturer");
                WriteLine($"{device.VendorID:X4}:{device.ProductID:X4}", "VendorID:ProductID");
                WriteLine(device.InternalFilePath, "InternalFilePath");
                WriteLine(device.GetIndexedString(2), "Index[2]");
            }
        }
    }
}
