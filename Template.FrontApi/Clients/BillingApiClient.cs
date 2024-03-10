using System.Text.Json;
using Template.FrontApi.Models;

namespace Template.FrontApi.Clients;

public class BillingApiClient
{
    private readonly HttpClient _client;

    public BillingApiClient() =>
        _client = new HttpClient();

    public async Task<Operation[]?> GetOperationLog(Guid userId)
    {
        var response = await GetAsync("operation-log?userId" + userId);
        return JsonSerializer.Deserialize<Operation[]>(await response.Content.ReadAsStreamAsync());
    }

    public async Task<decimal> GetCurrentBalance(Guid userId)
    {
        var response = await GetAsync("current-balance?userId" + userId);
        return JsonSerializer.Deserialize<decimal>(await response.Content.ReadAsStreamAsync());
    }
    
    public async Task<int?> GetRedEmployees()
    {
        var response = await GetAsync("red-employees");
        return JsonSerializer.Deserialize<int>(await response.Content.ReadAsStreamAsync());
    }

    public async Task<Guid?> GetMostExpensiveTaskOfDay()
    {
        var response = await GetAsync("most-expensive-task-of-day");
        return JsonSerializer.Deserialize<Guid?>(await response.Content.ReadAsStreamAsync());
    }

    public async Task<Guid?> GetMostExpensiveTaskOfWeek()
    {
        var response = await GetAsync("most-expensive-task-of-week");
        return JsonSerializer.Deserialize<Guid?>(await response.Content.ReadAsStreamAsync());
    }

    public async Task<Guid?> GetMostExpensiveTaskOfMonth()
    {
        var response = await GetAsync("most-expensive-task-of-month");
        return JsonSerializer.Deserialize<Guid?>(await response.Content.ReadAsStreamAsync());
    }

    async Task<HttpResponseMessage> GetAsync(string url)
    {
        using var response =
            await _client.GetAsync("http://localhost.dev.course/externalapi/billing/" + url);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
        return response;
    }
}