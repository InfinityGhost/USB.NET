namespace USB.NET.Packets
{
    public enum RequestType : byte
    {
        USB_DIR_OUT = 0,
        USB_DIR_IN = 0x80,
        USB_TYPE_MASK = (0x03 << 5),
        USB_TYPE_STANDARD = (0x00 << 5),
        USB_TYPE_CLASS = (0x01 << 5),
        USB_TYPE_VENDOR = (0x02 << 5),
        USB_TYPE_RESERVED = (0x03 << 5),
        USB_RECIP_MASK = 0x1f,
        USB_RECIP_DEVICE = 0x00,
        USB_RECIP_INTERFACE = 0x01,
        USB_RECIP_ENDPOINT = 0x02,
        USB_RECIP_OTHER = 0x03,
    }
}