using ApiContracts.DTOs;
using Entities;
using EntityRepository;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : Controller
{
    private readonly IPostRepo _postRepository;

    public PostController(IPostRepo postRepository)
    {
        _postRepository = postRepository;
    }


    [HttpGet]
    public IActionResult GetAllPosts()
    {
        var posts = _postRepository.GetManyAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null)
            return NotFound(); // 404 Not Found if the post doesn't exist
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostDTO postDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // 400 Bad Request

        var newPost = new Post
        {
            Title = postDto.Title,
            Body = postDto.Body,
            Id = 0,
            UserId = postDto.UserId,
            Upvotes = 0,
            Downvotes = 0,
            Content = postDto.Title + "\n" + postDto.Body
        };

        await _postRepository.AddAsync(newPost);
        return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id },
            newPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id,
        [FromBody] PostDTO postDto)
    {
        var postToUpdate = await _postRepository.GetSingleAsync(id);
        if (postToUpdate == null) return NotFound();

        postToUpdate.Title = postDto.Title;
        postToUpdate.Body = postDto.Body;
        postToUpdate.UserId = postDto.UserId;
        await _postRepository.UpdateAsync(postToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postRepository.GetSingleAsync(id);
        if (post == null) return NotFound();

        await _postRepository.DeleteAsync(id);
        return NoContent();
    }
    
   
}