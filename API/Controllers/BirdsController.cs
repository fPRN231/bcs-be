using API.Models;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/birds")]
public class BirdsController : BaseController
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBird(Guid id)
    {
        var target = await _birdRepostory.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBird([FromBody] CreateBirdRequest req)
    {
        Bird entity = Mapper.Map(req, new Bird());
        entity.UserId = CurrentUserID;
        await _birdRepostory.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBird(Guid id, [FromBody] UpdateBirdRequest req)
    {
        var target = await _birdRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Bird entity = Mapper.Map(req, target);
        await _birdRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBird(Guid id)
    {
        var target = await _birdRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _birdRepostory.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
