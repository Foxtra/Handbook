using Handbook.Services.DTO;
using Handbook.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace Handbook.Services.Implementation;

public sealed class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? new HttpClient();
    }
    public async Task<IReadOnlyCollection<CylinderDto>> LoadCylinders()
    {
        return await GetDataAsync<List<CylinderDto>>(Constants.Url.Cylinders);
    }

    public async Task<IReadOnlyCollection<DeviceTypeDto>> LoadDeviceTypes()
    {
        return await GetDataAsync<List<DeviceTypeDto>>(Constants.Url.DeviceTypes);
    }

    public async Task<IReadOnlyCollection<PumpDto>> LoadPumps()
    {
        return await GetDataAsync<List<PumpDto>>(Constants.Url.Pumps);
    }

    public async Task<IReadOnlyCollection<ValveDto>> LoadValves()
    {
        return await GetDataAsync<List<ValveDto>>(Constants.Url.Valves);
    }

    private async Task<T> GetDataAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            Console.WriteLine($"Неуспешный запрос по следующему по адресу: {url}");
            throw;
        }

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (result == null)
        {
            Console.WriteLine($"Пустой результат в результате десериализации json: {json}");            
        }

        return result;
    }
}
