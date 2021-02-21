using System;
using USB.NET.Descriptors;
using USB.NET.Platform.Windows.Enumerators;
using static Native.Windows.CfgMgr;
using static Native.Windows.Windows;

namespace USB.NET.Platform.Windows
{
    public sealed class UsbDevice : Device
    {
        public UsbDevice(int devNode)
        {
            CM_Get_Parent(ref parentNode, devNode, 0);
            Tools.RetrieveString(256, out var manufacturer, s =>
            {
                var size = s.Capacity;
                return CM_Get_DevNode_Property(devNode, ref manufacturerGuid, out _, s, ref size, 0);
            });
            Manufacturer = manufacturer;
            ProductName = "";
            InternalFilePath = "";
            Class = UsbEnumerator.GetNodeGuid(devNode);
        }

        private Guid Class;
        private int parentNode;
        private static DEVPROPKEY manufacturerGuid = DEVPKEY_Device_Manufacturer;

        public override void ClearFeature(ushort feature)
        {
            throw new System.NotImplementedException();
        }

        public override Configuration GetConfiguration()
        {
            throw new System.NotImplementedException();
        }

        public override DeviceDescriptor GetDeviceDescriptor()
        {
            throw new System.NotImplementedException();
        }

        public override string GetIndexedString(byte index)
        {
            throw new System.NotImplementedException();
        }

        public override bool SetConfiguration(ushort index)
        {
            throw new System.NotImplementedException();
        }

        public override void SetFeature(ushort feature)
        {
            throw new System.NotImplementedException();
        }
    }
}