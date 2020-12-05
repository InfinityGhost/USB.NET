using System;
using System.IO;
using System.Text.RegularExpressions;
using Native.Linux.libc;
using USB.NET.Descriptors;

namespace USB.NET.Platform.Linux
{
    using static Linux.Tools;
    using static libcMethods;

    internal unsafe class LinuxDeviceEndpoint : Endpoint
    {
        internal LinuxDeviceEndpoint(EndpointDescriptor descriptor, string hidrawPath)
        {
            this.descriptor = descriptor;
            var hidrawMatch = hidrawRegex.Match(hidrawPath);

            if (hidrawMatch.Success)
                this.hidrawPath = $"/dev/{hidrawMatch.Groups[1].Value}";
            else
                throw new Exception("Endpoint is not an hidraw endpoint.");

            var fd = open(this.hidrawPath, oflag.RDWR);
            if (fd > 0)
            {
                var res = ioctl(fd, HIDIOCGRDESCSIZE, out var len);
                if (res < 0)
                    throw CreateNativeException();
                
                base.InputReportLength = len;
            }
        }

        private EndpointDescriptor descriptor;
        private string hidrawPath;

        private const string HIDRAW_REGEX = "/hidraw/(.+?)$";
        private static readonly Regex hidrawRegex = new Regex(HIDRAW_REGEX, RegexOptions.Compiled);

        public override FileSystemInfo GetPath()
        {
            return new FileInfo(this.hidrawPath);
        }

        public override DeviceStream Open()
        {
            var fd = open(hidrawPath, oflag.RDWR | oflag.NONBLOCK);
            if (fd == -1)
                throw CreateNativeException();

            return new LinuxDeviceStream(this, fd);
        }

        public override EndpointDescriptor GetDescriptor()
        {
            return this.descriptor;
        }
    }
}