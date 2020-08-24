using System;
using System.Runtime.InteropServices;
using Native.Linux.Linux.USB;

namespace Native.Linux.libc
{
    public unsafe static class libcMethods
    {
        const string libc = "libc";

        [DllImport(libc, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        public static extern string strerror(int errno);

        [DllImport(libc, SetLastError = true)]
        public static extern int open(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string pathname,
            oflag flags
        );

        [DllImport(libc, SetLastError = true)]
        public static extern int open(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string pathname,
            oflag flags,
            uint mode
        );

        [DllImport(libc, SetLastError = true)]
        public static extern int creat(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string pathname,
            uint mode
        );

        [DllImport(libc, SetLastError = true)]
        public static extern int openat(
            int dirfd,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string pathname,
            uint mode
        );

        [DllImport(libc, SetLastError = true)]
        public static extern int openat(
            int dirfd,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string pathname,
            oflag flags,
            uint mode
        );

        [DllImport(libc, SetLastError = true)]
        public static extern int ioctl(int filedes, UIntPtr request, dynamic value);

        public const int IOC_NONE = 0;
        public const int IOC_WRITE = 1;
        public const int IOC_READ = 2;
        public const int IOC_NRBITS = 8;
        public const int IOC_TYPEBITS = 8;
        public const int IOC_SIZEBITS = 14;
        public const int IOC_DIRBITS = 2;
        public const int IOC_NRSHIFT = 0;
        public const int IOC_TYPESHIFT = IOC_NRSHIFT + IOC_NRBITS;
        public const int IOC_SIZESHIFT = IOC_TYPESHIFT + IOC_TYPEBITS;
        public const int IOC_DIRSHIFT = IOC_SIZESHIFT + IOC_SIZEBITS;

        public static UIntPtr IOC(int dir, int type, int nr, int size)
        {
            // Make sure to cast this to uint. We do NOT want this casted from int...
            uint value = (uint)dir << IOC_DIRSHIFT | (uint)type << IOC_TYPESHIFT | (uint)nr << IOC_NRSHIFT | (uint)size << IOC_SIZESHIFT;
            return (UIntPtr)value;
        }

        public static UIntPtr IOW(int type, int nr, int size)
        {
            return IOC(IOC_WRITE, type, nr, size);
        }

        public static UIntPtr IOR(int type, int nr, int size)
        {
            return IOC(IOC_READ, type, nr, size);
        }

        public static UIntPtr IOWR(int type, int nr, int size)
        {
            return IOC(IOC_WRITE | IOC_READ, type, nr, size);
        }

        public const int HID_MAX_DESCRIPTOR_SIZE = 4096;
        public static readonly UIntPtr HIDIOCGRDESCSIZE = IOR((byte)'H', 1, 4);
        public static readonly UIntPtr USBDEVFS_CONTROL = IOWR((byte)'U', 0, 8);
        public static UIntPtr HIDIOCGRAWPHYS(int length) => IOR((byte)'H', 5, length);
        public static UIntPtr HIDIOCSFEATURE(int length) => IOWR((byte)'H', 6, length);
        public static UIntPtr HIDIOCGFEATURE(int length) => IOWR((byte)'H', 7, length);
    }
}