using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using USB.NET.Platform.Windows.Exceptions;
using static Native.Windows.HID;
using static Native.Windows.Kernel32;
using static Native.Windows.SetupAPI;
using static Native.Windows.Windows;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal sealed class HIDDeviceEnumerator : WindowsDeviceCommonEnumerator
    {
        public HIDDeviceEnumerator()
        {
            GetHidDGuid(out var hidGuid);
            Initialize(DIGCF.DeviceInterface | DIGCF.Present, hidGuid);
        }

        private static readonly Regex interfaceRegex = new Regex("mi_(\\d\\d)", RegexOptions.Compiled);

        protected override bool ManyOutput => true;

        protected override Device ProcessDevice(SP_DEVINFO_DATA device)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<object> ProcessDeviceManyOutput(SP_DEVINFO_DATA device)
        {
            for (uint j = 0; ; j++)
            {
                var deviceInterface = new SP_DEVICE_INTERFACE_DATA { cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA)) };

                SetupDiEnumDeviceInterfaces(DeviceListPtr, ref device, ref Guid, j, ref deviceInterface);
                if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                    break;

                var fragment = ProcessInterface(deviceInterface);
                if (fragment != null)
                    yield return fragment;
            }
        }

        private HIDFragment ProcessInterface(SP_DEVICE_INTERFACE_DATA deviceInterface)
        {
            var interfaceInfo = new SP_DEVINFO_DATA { cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA)) };
            var interfaceDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA { cbSize = IntPtr.Size == 8 ? 8 : 6 };
            var size = (uint)Marshal.SizeOf(interfaceDetail);

            if (SetupDiGetDeviceInterfaceDetail(DeviceListPtr, ref deviceInterface, ref interfaceDetail, size, ref size, ref interfaceInfo))
            {
                return CreateHIDDevice(interfaceInfo, interfaceDetail);
            }
            else
            {
                throw new WindowsNativeException("Failed to get device interface details");
            }
        }

        private HIDFragment CreateHIDDevice(SP_DEVINFO_DATA interfaceInfo, SP_DEVICE_INTERFACE_DETAIL_DATA interfaceDetail)
        {
            var path = interfaceDetail.DevicePath;
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

                var deviceDesc = DEVPKEY_Device_InstanceId;
                var device = Tools.RetrieveString<WindowsNativeException>(256,
                    s => SetupDiGetDeviceProperty(DeviceListPtr, ref interfaceInfo, ref deviceDesc, out _, s, s.Capacity, out _, 0),
                    "Failed to get device property",
                    e => throw e
                );

                var instance = 0;
                if (CM_Locate_DevNode(ref instance, device, 0) != 0)
                    throw new Exception("Failed to locate device node");

                var parentInstance = 0;
                if (CM_Get_Parent(ref parentInstance, instance, 0) != 0)
                    throw new Exception("Failed to get parent node");

                var parentDevice = Tools.RetrieveString<Exception>(256,
                    s => CM_Get_Device_ID(parentInstance, s, s.Capacity, 0) == 0,
                    "Failed to get parent device",
                    e => throw e
                );

                var usbPath = $"\\\\?\\{parentDevice.Replace('\\', '#').ToLowerInvariant()}#{{f18a0e88-c30c-11d0-8815-00a0c906bed8}}";

                return new HIDFragment(deviceHandle, hidAttributes, hidCapabilities)
                {
                    HIDPath = path,
                    USBPath = usbPath
                };
            }
            else
            {
                return null;
            }
        }

        private static HIDDevice FinalizeHIDDevice(IGrouping<string, HIDFragment> usbDevice)
        {
            var mappedFragments = new Dictionary<int, List<HIDFragment>>();
            foreach (var fragment in usbDevice)
            {
                var interfaceNum = 0;
                var match = interfaceRegex.Match(fragment.HIDPath);
                if (match.Success)
                    if (!int.TryParse(match.Groups[1].Value, out interfaceNum))
                        throw new Exception("Failed to parse interface number");

                if (!mappedFragments.ContainsKey(interfaceNum))
                    mappedFragments.Add(interfaceNum, new List<HIDFragment>());

                mappedFragments[interfaceNum].Add(fragment);
            }
            return new HIDDevice(usbDevice.Key, mappedFragments);
        }

        public override IEnumerable<Device> Convert(IEnumerable<object> fragments)
        {
            var group = fragments.Select(f => (HIDFragment)f).GroupBy(d => d.USBPath);
            foreach (var usbDevice in group)
            {
                yield return FinalizeHIDDevice(usbDevice);
            }
        }
    }
}