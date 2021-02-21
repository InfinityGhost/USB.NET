using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Native.Windows.CfgMgr;
using static Native.Windows.SetupAPI;
using static Native.Windows.Windows;

namespace USB.NET.Platform.Windows.Enumerators
{
    internal class UsbEnumerator : IDeviceEnumerator
    {
        private readonly List<UsbDevice> usbDevices = new List<UsbDevice>();

        // Cache getters
        private static Guid usbHubGuid = GUID_DEVINTERFACE_USB_HUB;

        private static DEVPROPKEY instanceIdProp = DEVPKEY_Device_InstanceId;

        public UsbEnumerator()
        {
            usbDevices.Clear();
            var usbHubs = usbHubGuid;
            var deviceList = SetupDiGetClassDevs(ref usbHubs, IntPtr.Zero, IntPtr.Zero, DIGCF.DeviceInterface | DIGCF.Present);

            for (uint i = 0; ; i++)
            {
                var deviceInterface = new SP_DEVICE_INTERFACE_DATA { cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA)) };
                var deviceInterfaceInfo = new SP_DEVINFO_DATA { cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA)) };
                var deviceInterfaceDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA { cbSize = IntPtr.Size == 8 ? 8 : 6 };

                SetupDiEnumDeviceInterfaces(deviceList, IntPtr.Zero, ref usbHubs, i, ref deviceInterface);
                if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                    break;

                var size = (uint)Marshal.SizeOf(deviceInterfaceDetail);
                SetupDiGetDeviceInterfaceDetail(deviceList, ref deviceInterface, ref deviceInterfaceDetail, size, ref size, ref deviceInterfaceInfo);

                var deviceInstanceId = Tools.RetrieveString(256, s =>
                {
                    SetupDiGetDeviceProperty(deviceList, ref deviceInterfaceInfo, ref instanceIdProp, out _, s, s.Capacity, out _, 0);
                });

                var hubNode = 0;
                ExecuteWithRet(() => CM_Locate_DevNode(ref hubNode, deviceInstanceId, 0), ret =>
                {
                    if (ret != CR.SUCCESS)
                        throw new CfgMgrException("Failed to locate USB Hub", ret);
                });

                usbDevices.AddRange(ProcessHub(hubNode));
            }
        }

        public IEnumerable<Device> GetDevices()
        {
            return usbDevices;
        }

        private IEnumerable<UsbDevice> ProcessHub(int hubNode)
        {
            var nodeStack = new Stack<int>();
            nodeStack.Push(hubNode);

            while (nodeStack.Count > 0)
            {
                var currentNode = nodeStack.Pop();
                var childNode = currentNode;
                if (CM_Get_Child(ref childNode, currentNode, 0) == CR.SUCCESS)
                {
                    do
                    {
                        nodeStack.Push(childNode);
                    }
                    while (CM_Get_Sibling(ref childNode, childNode, 0) == CR.SUCCESS);
                }
                else
                {
                    yield return new UsbDevice(currentNode);
                }
            }
        }

        public static Guid GetNodeGuid(int node)
        {
            var actualGuid = new Guid();
            var GuidProperty = DEVPKEY_Device_ClassGuid;
            var BufferSize = Marshal.SizeOf(actualGuid);

            ExecuteWithRet(() => CM_Get_DevNode_Property(node, ref GuidProperty, out _, ref actualGuid, ref BufferSize, 0), ret =>
            {
                if (ret != CR.SUCCESS)
                    throw new CfgMgrException("Failed to get devNode property", ret);
            });
            return actualGuid;
        }

        public void Dispose()
        {
        }
    }
}