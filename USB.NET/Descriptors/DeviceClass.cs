namespace USB.NET.Descriptors
{
    public enum DeviceClass : byte
    {
        Generic = 0x00,
        Audio = 0x01,
        Communications = 0x02,
        HID = 0x03,
        Physical = 0x05,
        Image = 0x06,
        Printer = 0x07,
        MassStorage = 0x08,
        Hub = 0x09,
        CDCData = 0x0A,
        SmartCard = 0x0B,
        ContentSecurity = 0x0D,
        Video = 0x0E,
        PersonalHealthcare = 0x0F,
        AudioVideo = 0x10,
        BillboardDeviceClass = 0x11,
        USBTypeCBridgeClass = 0x12,
        Diagnostic = 0xDC,
        WirelessController = 0xE0,
        Miscellaneous = 0xEF,
        ApplicationSpecific = 0xFE,
        VendorSpecific = 0xFF,
    }
}