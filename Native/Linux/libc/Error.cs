namespace Native.Linux.libc
{
    public enum Error
    {
        OK = 0,
        EPERM = 1,
        EINTR = 4,
        EIO = 5,
        ENXIO = 6,
        EBADF = 9,
        EAGAIN = 11,
        EACCES = 13,
        EBUSY = 16,
        ENODEV = 19,
        EINVAL = 22
    }
}