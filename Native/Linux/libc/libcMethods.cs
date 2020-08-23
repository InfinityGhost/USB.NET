using System.Runtime.InteropServices;

namespace Native.Linux.libc
{
    public unsafe static class libcMethods
    {
        const string libc = "libc";

        [DllImport(libc, SetLastError = true)]
        public static extern int ioctl(int filedes, ulong request, ref void* value);
    }
}