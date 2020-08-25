namespace USB.NET
{
    public interface IFeature
    {
        void SetFeature(ushort feature);
        void ClearFeature(ushort feature);
    }
}