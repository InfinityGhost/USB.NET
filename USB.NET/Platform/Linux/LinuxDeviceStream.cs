using System;
using System.IO;
using Native.Linux.libc;
using Native.Linux.sys;
using USB.NET.Platform.Linux.Exceptions;

namespace USB.NET.Platform.Linux
{
    using static Linux.Tools;
    using static libcMethods;

    internal unsafe class LinuxDeviceStream : DeviceStream
    {
        public LinuxDeviceStream(LinuxDeviceEndpoint endpoint, int fd)
        {
            this.endpoint = endpoint;
            this.fd = fd;
        }

        private LinuxDeviceEndpoint endpoint;
        private int fd;

        private byte[] buffer;

        public override byte[] Read()
        {
            buffer = new byte[endpoint.InputReportLength];
            var len = Read(buffer, 0, (int)endpoint.InputReportLength);
            return buffer[0..len];
        }

        public override void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            fixed (void* ptr = buffer)
            {
                Error err = Error.EAGAIN;
                while (err == Error.EAGAIN)
                {
                    WaitForInput(TimeSpan.FromMilliseconds(250));

                    var len = read(fd, ptr, (uint)count);
                    if (len >= 0)
                        return len;
                    else
                        err = GetError();
                }
                throw CreateNativeException();
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            fixed (void* ptr = buffer)
            {
                Error err = Error.EAGAIN;
                while (err == Error.EAGAIN)
                {
                    if (write(fd, ptr, (uint)count) >= 0)
                        return;
                    else
                        err = GetError();
                }
                throw CreateNativeException();
            }
        }

        private void WaitForInput(TimeSpan timeout)
        {
            var pfd = new pollfd
            {
                fd = fd,
                events = pollevent.IN
            };
            if (poll(ref pfd, 1, timeout.Milliseconds) < 0)
                throw CreateNativeException();
        }

        private void WaitForOutput(TimeSpan timeout)
        {
            var pfd = new pollfd
            {
                fd = fd,
                events = pollevent.OUT
            };
            if (poll(ref pfd, 1, timeout.Milliseconds) < 0)
                throw CreateNativeException();
        }

        public override void SetFeature(ushort feature)
        {
            throw new NotImplementedException();
        }

        public override void ClearFeature(ushort feature)
        {
            throw new NotImplementedException();
        }

        ~LinuxDeviceStream()
        {
            close(fd);
        }
    }
}