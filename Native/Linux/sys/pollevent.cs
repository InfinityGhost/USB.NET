using System;

namespace Native.Linux.sys
{
    [Flags]
    public enum pollevent : short
    {
        IN = 0x01,
        PRI = 0x02,
        OUT = 0x04,
        ERR = 0x08,
        HUP = 0x10,
        NVAL = 0x20
    }
}