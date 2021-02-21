using System;
using System.Runtime.InteropServices;
using System.Text;
using static Native.Windows.Windows;

namespace Native.Windows
{
    public static partial class CfgMgr
    {
        public delegate int CM_NOTIFY_CALLBACK(IntPtr hNotify, IntPtr Context, CM_NOTIFY_ACTION Action, ref CM_NOTIFY_EVENT_DATA EventData, int EventDataSize);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Locate_DevNode(ref int pdnDevInst, string pDeviceID, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_Device_ID(int dnDevInst, StringBuilder Buffer, int BufferLen, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_Parent(ref int pdnDevInst, int dnDevInst, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_Child(ref int pdnDevInst, int dnDevInst, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_Sibling(ref int pdnDevInst, int dnDevInst, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Enumerate_Classes(int ulClassIndex, ref Guid ClassGuid, CM_ENUMERATE_CLASSES flags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Register_Notification(ref CM_NOTIFY_FILTER pFilter, IntPtr pContext, CM_NOTIFY_CALLBACK pCallback, ref int pNotifyContext);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Unregister_Notification(ref int NotifyContext);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_DevNode_Property(int dnDevInst, ref DEVPROPKEY PropertyKey, out int PropertyType, StringBuilder PropertyBuffer, ref int PropertyBufferSize, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_DevNode_Property(int dnDevInst, ref DEVPROPKEY PropertyKey, out int PropertyType, ref Guid PropertyBuffer, ref int PropertyBufferSize, int ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Unicode)]
        public static extern CR CM_Get_DevNode_Property(int dnDevInst, ref DEVPROPKEY PropertyKey, out int PropertyType, IntPtr PropertyBuffer, ref int PropertyBufferSize, int ulFlags);
    }
}