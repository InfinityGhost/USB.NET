namespace USB.NET.Packets
{
    public enum Request : byte
    {
        GET_STATUS = 0x00,
        CLEAR_FEATURE = 0x01,
        SET_FEATURE = 0x03,
        SET_ADDRESS = 0x05,
        GET_DESCRIPTOR = 0x06,
        SET_DESCRIPTOR = 0x07,
        GET_CONFIGURATION = 0x08,
        SET_CONFIGURATION = 0x09,
        GET_INTERFACE = 0x0A,
        SET_INTERFACE = 0x0B,
        SYNCH_FRAME = 0x0C,
        SET_SEL = 0x30,
        SET_ISOCH_DELAY = 0x31,
    }
}