using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Native.Linux.libc;
using USB.NET.Descriptors;

namespace USB.NET.Platform.Linux
{
    using static libcMethods;

    internal unsafe class LinuxDeviceEndpoint : Endpoint
    {
        internal LinuxDeviceEndpoint(EndpointDescriptor descriptor, string hidrawPath)
        {
            this.descriptor = descriptor;
            this.hidrawPath = hidrawPath;
        }

        private EndpointDescriptor descriptor;
        private string hidrawPath;

        public override FileSystemInfo GetPath()
        {
            return new FileInfo(this.hidrawPath);
        }

        public override Stream Open()
        {
            // This is likely wrong, but we do need RDWR | NONBLOCK
            var streamHandle = open(hidrawPath, oflag.RDWR | oflag.NONBLOCK);
            var safefileHandle = new SafeFileHandle(new IntPtr(&streamHandle), true);
            return new FileStream(safefileHandle, FileAccess.ReadWrite);
        }

        public override void SetFeature(ushort feature)
        {
            throw new System.NotImplementedException();
        }

        public override void ClearFeature(ushort feature)
        {
            throw new System.NotImplementedException();
        }

        public override EndpointDescriptor GetDescriptor()
        {
            return this.descriptor;
        }
    }
}