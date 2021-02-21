namespace Native.Windows
{
    public static partial class SetupAPI
    {
        public enum SPDRP : uint
        {
            DEVICEDESC = 0x00000000, // DeviceDesc (R/W)
            HARDWAREID = 0x00000001, // HardwareID (R/W)
            COMPATIBLEIDS = 0x00000002, // CompatibleIDs (R/W)
            UNUSED0 = 0x00000003, // unused
            SERVICE = 0x00000004, // Service (R/W)
            UNUSED1 = 0x00000005, // unused
            UNUSED2 = 0x00000006, // unused
            CLASS = 0x00000007, // Class (R--tied to ClassGUID)
            CLASSGUID = 0x00000008, // ClassGUID (R/W)
            DRIVER = 0x00000009, // Driver (R/W)
            CONFIGFLAGS = 0x0000000A, // ConfigFlags (R/W)
            MFG = 0x0000000B, // Mfg (R/W)
            FRIENDLYNAME = 0x0000000C, // FriendlyName (R/W)
            LOCATION_INFORMATION = 0x0000000D, // LocationInformation (R/W)
            PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E, // PhysicalDeviceObjectName (R)
            CAPABILITIES = 0x0000000F, // Capabilities (R)
            UI_NUMBER = 0x00000010, // UiNumber (R)
            UPPERFILTERS = 0x00000011, // UpperFilters (R/W)
            LOWERFILTERS = 0x00000012, // LowerFilters (R/W)
            BUSTYPEGUID = 0x00000013, // BusTypeGUID (R)
            LEGACYBUSTYPE = 0x00000014, // LegacyBusType (R)
            BUSNUMBER = 0x00000015, // BusNumber (R)
            ENUMERATOR_NAME = 0x00000016, // Enumerator Name (R)
            SECURITY = 0x00000017, // Security (R/W, binary form)
            SECURITY_SDS = 0x00000018, // Security (W, SDS form)
            DEVTYPE = 0x00000019, // Device Type (R/W)
            EXCLUSIVE = 0x0000001A, // Device is exclusive-access (R/W)
            CHARACTERISTICS = 0x0000001B, // Device Characteristics (R/W)
            ADDRESS = 0x0000001C, // Device Address (R)
            UI_NUMBER_DESC_FORMAT = 0X0000001D, // UiNumberDescFormat (R/W)
            DEVICE_POWER_DATA = 0x0000001E, // Device Power Data (R)
            REMOVAL_POLICY = 0x0000001F, // Removal Policy (R)
            REMOVAL_POLICY_HW_DEFAULT = 0x00000020, // Hardware Removal Policy (R)
            REMOVAL_POLICY_OVERRIDE = 0x00000021, // Removal Policy Override (RW)
            INSTALL_STATE = 0x00000022, // Device Install State (R)
            LOCATION_PATHS = 0x00000023, // Device Location Paths (R)
            BASE_CONTAINERID = 0x00000024  // Base ContainerID (R)
        }
    }
}