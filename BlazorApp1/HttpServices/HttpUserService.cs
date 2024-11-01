// File: HttpServices/HttpUserService.cs

using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices;

public class HttpUserService : IUserService
{
    private readonly HttpClient _client;

    public HttpUserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserDto> AddUserAsync(UserDto request)
    {
        var response = await _client.PostAsJsonAsync("users", request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException();
    }

    public async Task<UserDto> GetUserAsync(int id)
    {
        return await _client.GetFromJsonAsync<UserDto>($"users/{id}") ??
               throw new InvalidOperationException();
    }

    public async Task UpdateUserAsync(int id, UserDto request)
    {
        var response = await _client.PutAsJsonAsync($"users/{id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteUserAsync(int id)
    {
        var response = await _client.DeleteAsync($"users/{id}");
        response.EnsureSuccessStatusCode();
    }
}