using System.IO;
using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Endpoint
    {
        public virtual uint InputReportLength { protected set; get; }
        public virtual uint OutputReportLength { protected set; get; }
        public virtual uint FeatureReportLength { protected set; get; }

        public abstract FileSystemInfo GetPath();
        public abstract DeviceStream Open();
        public abstract EndpointDescriptor GetDescriptor();
    }
}