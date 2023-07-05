using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/services")]
public class UsersController : Controller
{
    private readonly IRepositoryBase<User> _userRepostory;

    public UsersController(IRepositoryBase<User> userRepostory)
    {
        _userRepostory = userRepostory;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _userRepostory.ToListAsync());
    }
}
