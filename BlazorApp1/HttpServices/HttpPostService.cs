using BlazorApp1;
using BlazorApp1.Components;
using BlazorApp1.HttpServices;
using System.Net.Http;
using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices;

public class HttpPostService : IPostService
{
    private readonly HttpClient _client;

    public HttpPostService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<PostDTO>> GetPostsAsync(int id)
    {
        var response = await _client.GetAsync($"users/{id}/posts");
        response.EnsureSuccessStatusCode();
        var posts = await _client.GetFromJsonAsync<List<PostDTO>>($"users/{id}/posts");
        return posts ?? new List<PostDTO>();
    }

    public async Task<PostDTO> AddPostAsync(int userId, PostDTO post)
    {
        var response =
            await _client.PostAsJsonAsync($"api/users/{userId}/posts", post);
        response.EnsureSuccessStatusCode();
        return await _client.GetFromJsonAsync<PostDTO>(
            $"api/users/{userId}/posts/{response.Headers.GetValues("id").First()}") ?? throw new InvalidOperationException();
    }

    public async Task UpdatePostAsync(int userId, int postId, PostDTO post)
    {
        var response =
            await _client.PutAsJsonAsync($"api/users/{userId}/posts/{postId}",
                post);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePostAsync(int userId, int postId)
    {
        var response =
            await _client.DeleteAsync($"api/users/{userId}/posts/{postId}");
    }
}