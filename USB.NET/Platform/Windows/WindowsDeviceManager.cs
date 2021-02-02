using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using USB.NET.Platform.Windows.Exceptions;
using static Native.Windows.HID;
using static Native.Windows.Kernel32;
using static Native.Windows.SetupAPI;
using static Native.Windows.Windows;

namespace USB.NET.Platform.Windows
{
    public unsafe sealed class WindowsDeviceManager : DeviceManager, IDisposable
    {
        public override IEnumerable<Device> GetAllDevices()
        {
            GetHidDGuid(out var hidGuid);

            var deviceInfo = SetupDiGetClassDevs(ref hidGuid, IntPtr.Zero, IntPtr.Zero, DIGCF.DeviceInterface | DIGCF.Present);
            if (deviceInfo == InvalidHandle)
                throw new WindowsNativeException("Failed to retrieve device information set");

            var deviceInfoData = new SP_DEVINFO_DATA
            {
                cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA))
            };

            var interfaceInfoData = new SP_DEVINFO_DATA
            {
                cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA))
            };

            var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA
            {
                cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA))
            };

            for (uint i = 0; SetupDiEnumDeviceInfo(deviceInfo, i, ref deviceInfoData); i++)
            {
                for (uint j = 0; SetupDiEnumDeviceInterfaces(deviceInfo, ref deviceInfoData, ref hidGuid, j, ref deviceInterfaceData); j++)
                {
                    var deviceInterfaceDetailData = new SP_DEVICE_INTERFACE_DETAIL_DATA
                    {
                        cbSize = IntPtr.Size == 8 ? 8 : 6
                    };

                    uint size = 0;
                    SetupDiGetDeviceInterfaceDetail(deviceInfo, ref deviceInterfaceData, IntPtr.Zero, 0, ref size, IntPtr.Zero);
                    if (SetupDiGetDeviceInterfaceDetail(deviceInfo, ref deviceInterfaceData, ref deviceInterfaceDetailData, size, ref size, ref interfaceInfoData))
                    {
                        SetupDiEnumDeviceInfo(deviceInfo, j, ref interfaceInfoData);
                        var path = deviceInterfaceDetailData.DevicePath;
                        var deviceHandle = CreateFile(
                            path,
                            FileAccess.Read,
                            FileShare.Read,
                            IntPtr.Zero,
                            FileMode.Open,
                            FileAttributes.Device,
                            IntPtr.Zero
                        );

                        if (deviceHandle != InvalidHandle)
                        {
                            var hidAttributes = new HIDD_ATTRIBUTES();
                            GetAttributes(deviceHandle, ref hidAttributes);
                            GetPreparsedData(deviceHandle, out var hidPreparsedData);
                            GetCaps(hidPreparsedData, out var hidCapabilities);

                            FreePreparsedData(hidPreparsedData);

                            var buffer = new byte[1024];
                            if (!SetupDiGetDeviceRegistryProperty(deviceInfo, ref deviceInfoData, SPDRP.LOCATION_INFORMATION, out var a, buffer, 1024, out var b))
                                throw new WindowsNativeException("Failed to get device registry property");

                            yield return new WindowsDevice(deviceHandle, path, hidAttributes, hidCapabilities)
                            {
                                Location = Encoding.Default.GetString(buffer)
                            };
                        }
                    }
                    else
                    {
                        throw new WindowsNativeException("Failed to get device interface details");
                    }
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}