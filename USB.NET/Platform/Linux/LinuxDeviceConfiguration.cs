using USB.NET.Descriptors;

namespace USB.NET.Platform.Linux
{
    public sealed class LinuxDeviceConfiguration : Configuration
    {
        internal LinuxDeviceConfiguration(ConfigurationDescriptor descriptor, string devname)
        {
            this.devname = devname;
            this.descriptor = descriptor;
            SetValues(descriptor);
        }

        private ConfigurationDescriptor descriptor;
        private string devname;

        public override uint InterfaceCount { protected set; get; }

        public override Interface GetInterface(int index)
        {
            throw new System.NotImplementedException();
        }

        public override ConfigurationDescriptor GetConfigurationDescriptor()
        {
            return this.descriptor;
        }
    }
}