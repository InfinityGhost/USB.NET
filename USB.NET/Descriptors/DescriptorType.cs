namespace USB.NET.Descriptors
{
    public enum DescriptorType : byte
    {
        Device = 1,
        Configuration = 2,
        String = 3,
        Interface = 4,
        Endpoint = 5,
        DeviceQualifier = 6,
        SpeedConfiguration = 7,
        OTG = 9
    }
}