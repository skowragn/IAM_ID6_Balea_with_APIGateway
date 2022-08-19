namespace Assets.Management.Common.Interfaces;
public interface IPatientsServiceRepository
{
    Task<string> GetPatients(string accessToken, string? requestUri);
}