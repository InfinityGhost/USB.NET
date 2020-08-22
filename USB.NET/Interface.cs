namespace USB.NET
{
    public abstract class Interface
    {
        public abstract uint EndpointCount { get; }
        public abstract Endpoint GetEndpoint(uint index);
    }
}