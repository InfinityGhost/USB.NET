using System.IO;

namespace USB.NET
{
    public abstract class Endpoint
    {
        public abstract uint InputReportLength { get; }
        public abstract uint OutputReportLength { get; }
        public abstract uint FeatureReportLength { get; }

        public abstract FileSystemInfo GetPath();
        public abstract Stream Open();
    }
}