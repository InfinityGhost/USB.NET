using USB.NET.Descriptors;

namespace USB.NET.Platform.Linux
{
    internal sealed class LinuxDeviceConfiguration : Configuration
    {
        internal LinuxDeviceConfiguration(ConfigurationDescriptor descriptor, string devname, byte[] otherDescriptors = null)
        {
            this.devname = devname;
            this.descriptor = descriptor;
            this.otherDescriptors = otherDescriptors;
            SetValues(descriptor);
        }

        private ConfigurationDescriptor descriptor;
        private string devname;
        private byte[] otherDescriptors;

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