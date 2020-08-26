using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Interface
    {
        protected virtual void SetValues(InterfaceDescriptor descriptor)
        {
            this.InterfaceValue = descriptor.bInterfaceNumber;
            this.EndpointCount = descriptor.bNumEndpoints;
        }

        public virtual uint InterfaceValue { protected set; get; }
        public virtual uint EndpointCount { protected set; get; }
        public abstract Endpoint GetEndpoint(uint index);

        public abstract InterfaceDescriptor GetDescriptor();
    }
}