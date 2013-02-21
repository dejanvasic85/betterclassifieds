namespace Paramount.Services
{
    public interface IServiceInformation
    {
        string ApplicationName { get; }
        string Version { get; }
    }
}