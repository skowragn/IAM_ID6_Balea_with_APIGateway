using System.Net.Http.Headers;
using System.Text.Json;
using Assets.Management.Common.Interfaces;

namespace Assets.Management.Infrastructure.Repositories;

public class PatientsServiceRepository : IPatientsServiceRepository
{
    public async Task<string> GetPatients(string accessToken, string? requestUri)
    {
        var client = HttpClientFactory.Create();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var content = await client.GetStringAsync(requestUri);
        var doc = JsonDocument.Parse(content).RootElement;
        var json = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });

        return json;
    }
}