using System;
using System.Diagnostics;
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
                    WriteLine(configuration.ConfigurationValue, "Configuration Value");
                    WriteLine(configuration.InterfaceCount, "Interface Count");
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }
        }

        [TestMethod]
        public void GetInterfaceTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                try
                {
                    WriteDeviceInfo(device);
                    var configuration = device.GetConfiguration();
                    var devInterface = configuration.GetInterface();
                    WriteLine(devInterface.InterfaceValue, "Interface Value");
                    WriteLine(devInterface.EndpointCount, "Endpoint Count");
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }
        }

        [TestMethod]
        public void GetEndpointTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                try
                {
                    WriteDeviceInfo(device);
                    var configuration = device.GetConfiguration();
                    var devInterface = configuration.GetInterface();
                    var endpoint = devInterface.GetEndpoint(1);
                    WriteLine(endpoint.GetPath(), "Endpoint hidraw path");
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }
        }

        [TestMethod]
        public void ReadEndpointTest()
        {
            foreach (var device in Host.DeviceManager.GetAllDevices())
            {
                try
                {
                    var configuration = device.GetConfiguration();
                    var devInterface = configuration.GetInterface();
                    var endpoint = devInterface.GetEndpoint(1);
                    WriteDeviceInfo(device);
                    using (var fs = endpoint.Open())
                    {
                        while (fs.CanRead)
                        {
                            var data = fs.Read();
                            WriteLine(BitConverter.ToString(data));
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }
        }
    }
}
