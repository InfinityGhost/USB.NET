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

        const ushort vid = 0x28bd;
        const ushort pid = 0x0914;

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
                for (byte i = 1; i < 255; i++)
                    WriteLine(device.GetIndexedString(i), $"Index[{i}]");
            }
        }
    }
}
