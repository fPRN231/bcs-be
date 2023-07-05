using API.Models.Request;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/doctor-logtimes")]
public class DoctorLogTimesController : BaseController
{
    private readonly IRepositoryBase<DoctorLogTime> _doctorLogTimeRepository;

    public DoctorLogTimesController(IRepositoryBase<DoctorLogTime> doctorLogTimeRepository)
    {
        _doctorLogTimeRepository = doctorLogTimeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctorLogTimes()
    {
        return Ok(await _doctorLogTimeRepository.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorLogTime(Guid id)
    {
        var target = await _doctorLogTimeRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctorLogTime([FromBody] CreateDoctorInfoRequest req)
    {
        DoctorLogTime entity = Mapper.Map(req, new DoctorLogTime());
        entity.DoctorId = CurrentUserID;
        await _doctorLogTimeRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorLogTime(Guid id, [FromBody] UpdateDoctorInfoRequest req)
    {
        var target = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        DoctorLogTime entity = Mapper.Map(req, target);
        await _doctorLogTimeRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorInfo(Guid id)
    {
        var target = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _doctorLogTimeRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
