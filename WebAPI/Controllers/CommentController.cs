using EntityRepository;
using ApiContracts.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CommentController : Controller
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    [HttpGet]
    public IActionResult GetAllComments()
    {
        var comments = _commentRepository.GetManyAsync(userId: 1); // TODO: replace with actual user id and fix 
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment == null)
            return NotFound(); // 404 Not Found if the comment doesn't exist
        return Ok(comment);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // 400 Bad Request
        var comment = new Comment
        {
            UserId = commentDTO.UserId,
            PostId = commentDTO.PostId,
            TextContent = commentDTO.TextContent,
            CreatedAt = commentDTO.CreatedAt,
            Upvotes = commentDTO.Upvotes,
            Downvotes = commentDTO.Downvotes,
        };

        await _commentRepository.AddAsync( comment);
        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDTO comment)
    {
        var commentToUpdate = await _commentRepository.GetSingleAsync(id);
        if (commentToUpdate == null) return NotFound();

        commentToUpdate.TextContent= comment.TextContent;
        await _commentRepository.UpdateAsync(commentToUpdate);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _commentRepository.GetSingleAsync(id);
        if (comment == null) return NotFound();

        await _commentRepository.DeleteAsync(id);
        return NoContent();
    }

}