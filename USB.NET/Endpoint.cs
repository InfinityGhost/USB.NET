using System.IO;
using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Endpoint : IFeature
    {
        public virtual uint InputReportLength { protected set; get; }
        public virtual uint OutputReportLength { protected set; get; }
        public virtual uint FeatureReportLength { protected set; get; }

        public abstract FileSystemInfo GetPath();
        public abstract Stream Open();

        public abstract void SetFeature(ushort feature);
        public abstract void ClearFeature(ushort feature);
        public abstract EndpointDescriptor GetDescriptor();
    }
}