using System;

namespace Native.Linux.libc
{
    [Flags]
    public enum oflag
    {
        RDONLY = 0x000,
        WRONLY = 0x001,
        RDWR = 0x002,
        CREAT = 0x040,
        EXCL = 0x080,
        NOCTTY = 0x100,
        TRUNC = 0x200,
        APPEND = 0x400,
        NONBLOCK = 0x800
    }
}