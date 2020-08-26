using USB.NET.Descriptors;

namespace USB.NET.Platform.Linux
{
    internal class LinuxDeviceInterface : Interface
    {
        internal LinuxDeviceInterface(InterfaceDescriptor descriptor, string devname, byte[] otherDescriptors = null)
        {
            this.descriptor = descriptor;
            this.devname = devname;
            this.otherDescriptors = otherDescriptors;
            SetValues(descriptor);
        }

        private InterfaceDescriptor descriptor;
        private string devname;
        private byte[] otherDescriptors;

        public override Endpoint GetEndpoint(uint index)
        {
            throw new System.NotImplementedException();
        }

        public override InterfaceDescriptor GetDescriptor()
        {
            return this.descriptor;
        }
    }
}