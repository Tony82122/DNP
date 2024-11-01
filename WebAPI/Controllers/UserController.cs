using Microsoft.AspNetCore.Mvc;
using EntityRepository;
using Entities;
using ApiContracts.DTOs;


namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]


public class UserController : Controller
{
    private readonly IUserRepo _userRepository;
    public UserController(IUserRepo userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userRepository.GetManyAsync();
        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetSingleAsync(id);
        if (user == null)
            return NotFound(); // 404 Not Found if the user doesn't exist
        return Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // 400 Bad Request

        var newUser = new User
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            Id = 0,
            Password = userDto.Password,
            Joined = DateTime.Now,
            Subscribes = new List<int>() // TODO: replace with actual subscribes list
            
        };

        await _userRepository.AddAsync(newUser);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id },
            newUser);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
    {
        var userToUpdate = await _userRepository.GetSingleAsync(id);
        if (userToUpdate == null) return NotFound();

        userToUpdate.UserName = userDto.UserName;
        userToUpdate.Email = userDto.Email;
        userToUpdate.Password = userDto.Password;
        await _userRepository.UpdateAsync(userToUpdate);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetSingleAsync(id);
        if (user == null) return NotFound();

        await _userRepository.DeleteAsync(id);
        return NoContent();
    }
    
    
}