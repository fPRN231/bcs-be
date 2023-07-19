using API.Models.Request.Users;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/users")]
public class UsersController : BaseController
{
    private readonly IRepositoryBase<User> _userRepostory;

    public UsersController(IRepositoryBase<User> userRepostory)
    {
        _userRepostory = userRepostory;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userRepostory.ToListAsync());
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        return Ok(await _userRepostory.WhereAsync(x=> x.Role.Equals(Role.Doctor)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var target = await _userRepostory.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest req)
    {
        User entity = Mapper.Map(req, new User());
        await _userRepostory.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest req)
    {
        var target = await _userRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        User entity = Mapper.Map(req, target);
        await _userRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var target = await _userRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Soft Delete
        await _userRepostory.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
