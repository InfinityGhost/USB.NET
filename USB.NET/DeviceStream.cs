using System;
using System.IO;

namespace USB.NET
{
    public abstract class DeviceStream : Stream, IFeature
    {

        public abstract byte[] Read();
        public abstract void Write(byte[] data);

        public abstract void SetFeature(ushort feature);
        public abstract void ClearFeature(ushort feature);

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            set => throw new NotSupportedException();
            get => throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
        
        public override void Flush()
        {
            throw new NotSupportedException();
        }
    }
}