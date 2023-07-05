using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/birds")]
public class BirdsController : Controller
{
    private readonly IRepositoryBase<Bird> _birdRepostory;

    public BirdsController(IRepositoryBase<Bird> birdRepostory)
    {
        _birdRepostory = birdRepostory;
    }

    [HttpGet]
    public async Task<IActionResult> GetBirds()
    {
        return Ok(await _birdRepostory.ToListAsync());
    }
}
