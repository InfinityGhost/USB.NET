using System.IO;

namespace USB.NET
{
    public abstract class Endpoint : IFeature
    {
        public abstract uint InputReportLength { get; }
        public abstract uint OutputReportLength { get; }
        public abstract uint FeatureReportLength { get; }

        public abstract FileSystemInfo GetPath();
        public abstract Stream Open();

        public abstract void SetFeature(ushort feature);
        public abstract void ClearFeature(ushort feature);
    }
}