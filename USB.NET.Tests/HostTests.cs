using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace USB.NET.Tests
{
    using static Trace;

    [TestClass]
    public class HostTests
    {
        private static void WriteDeviceInfo(Device device)
        {
            WriteLine(device.ProductName, "ProductName");
            WriteLine(device.Manufacturer, "Manufacturer");
            WriteLine($"{device.VendorID:X4}:{device.ProductID:X4}", "VendorID:ProductID");
            WriteLine(device.InternalFilePath, "InternalFilePath");
        }

        [TestMethod]
        public void GetAllDevicesTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                WriteDeviceInfo(device);
                WriteLine(string.Empty);
            }
        }

        [TestMethod]
        public void GetDeviceStringTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                WriteDeviceInfo(device);
                try
                {
                    for (byte i = 1; i < 255; i++)
                        WriteLine(device.GetIndexedString(i), $"Index[{i}]");
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }
        }

        [TestMethod]
        public void GetConfigurationTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                try
                {
                    WriteDeviceInfo(device);
                    var configuration = device.GetConfiguration();
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }
        }
    }
}
