using System.Text.Json;
using Template.FrontApi.Models;

namespace Template.FrontApi.Clients;

public class AuthApiClient
{
    private readonly HttpClient _client;

    public AuthApiClient() =>
        _client = new HttpClient();

    public async Task<User[]?> GetUsersForAssign()
    {
        var users = await GetAsync();
        return users;
    }

    async Task<User[]?> GetAsync()
    {
        using var response =
            await _client.GetAsync("http://localhost.dev.course/externalapi/users/for-assign");

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        return JsonSerializer.Deserialize<User[]>(await response.Content.ReadAsStreamAsync());
    }
}