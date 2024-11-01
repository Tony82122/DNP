using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient _client;

    public HttpCommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<CommentDTO> AddCommentAsync(int postId,
        CommentDTO comment)
    {
        var response =
            await _client.PostAsJsonAsync($"api/posts/{postId}/comments",
                comment);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CommentDTO>() ??
               throw new InvalidOperationException(
                   "Failed to retrieve the created comment.");
    }

    public async Task<CommentDTO> GetCommentAsync(int postId, int commentId)
    {
        return await _client.GetFromJsonAsync<CommentDTO>(
                   $"api/posts/{postId}/comments/{commentId}") ??
               throw new InvalidOperationException();
    }

    public async Task UpdateCommentAsync(int postId, int commentId,
        CommentDTO request)
    {
        var response =
            await _client.PutAsJsonAsync(
                $"api/posts/{postId}/comments/{commentId}",
                request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCommentAsync(int postId, int commentId)
    {
        var response =
            await _client.DeleteAsync(
                $"api/posts/{postId}/comments/{commentId}");
        response.EnsureSuccessStatusCode();
    }
}