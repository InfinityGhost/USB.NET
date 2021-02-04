using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Native.Windows
{
    public static partial class SetupAPI
    {
        public const int ERROR_NO_MORE_ITEMS = 259;

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, [MarshalAs(UnmanagedType.LPWStr)] string enumerator, IntPtr hwndParent, DIGCF flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, IntPtr enumerator, IntPtr hwndParent, DIGCF flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, ref SP_DEVINFO_DATA devInfo, ref Guid interfaceClassGuid, uint memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, ref SP_DEVINFO_DATA devInfo, IntPtr interfaceClassGuid, uint memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInfo(IntPtr hDevInfo, uint memberIndex, ref SP_DEVINFO_DATA deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, uint memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData, uint deviceInterfaceDetailDataSize, ref uint requiredSize, ref SP_DEVINFO_DATA deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, uint deviceInterfaceDetailDataSize, ref uint requiredSize, IntPtr deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA deviceInfoData, [MarshalAs(UnmanagedType.U4)] SPDRP property, out uint propertyRegDataType, byte[] propertyBuffer, uint propertyBufferSize, out uint requiredSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA deviceInfoData, [MarshalAs(UnmanagedType.U4)] SPDRP property, out uint propertyRegDataType, IntPtr propertyBuffer, uint propertyBufferSize, out uint requiredSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA deviceInfoData, [MarshalAs(UnmanagedType.U4)] SPDRP property, out uint propertyRegDataType, StringBuilder propertyBuffer, uint propertyBufferSize, out uint requiredSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe bool SetupDiGetDeviceProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref DEVPROPKEY propertyKey, out ulong propertyType, IntPtr propertyBuffer, int propertyBufferSize, out int requiredSize, uint flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe bool SetupDiGetDeviceProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref DEVPROPKEY propertyKey, out ulong propertyType, StringBuilder propertyBuffer, int propertyBufferSize, out int requiredSize, uint flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int CM_Locate_DevNode(ref int pdnDevInst, string pDeviceID, int ulFlags);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int CM_Get_Device_ID(int dnDevInst, StringBuilder Buffer, int BufferLen, int ulFlags);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int CM_Get_Parent(ref int pdnDevInst, int dnDevInst, int ulFlags);
    }
}