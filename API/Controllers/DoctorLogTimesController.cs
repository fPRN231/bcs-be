using API.Models.Request.DoctorInfos;
using API.Models.Request.DoctorLogTimes;
using AutoWrapper.Filters;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers;

[Authorize]
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

    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetAvailableLogTimes(Guid doctorId)
    {
        IOrderedEnumerable<DoctorLogTime> availableLogTimes;
        availableLogTimes = (await _doctorLogTimeRepository.WhereAsync(x => x.DoctorId.Equals(doctorId)))
                                                           .OrderBy(c => c.StartTime);
        return Ok(availableLogTimes);
    }

    [Authorize(Roles = PolicyName.DOCTOR)]
    [HttpPost]
    public async Task<IActionResult> CreateDoctorLogTime([FromBody] CreateDoctorLogTimeRequest req)
    {
        if (req.StartTime >= req.EndTime)
            throw new BadRequestException("Start time < End time wtf??");

        var checkTime = await _doctorLogTimeRepository.FirstOrDefaultAsync(
            dlt => dlt.DoctorId.Equals(CurrentUserID) && dlt.DayOfWeek == req.DayOfWeek &&
                (dlt.StartTime <= req.StartTime && dlt.EndTime >= req.EndTime) ||
                (dlt.StartTime > req.StartTime && dlt.StartTime < req.EndTime) ||
                (dlt.EndTime > req.StartTime && dlt.EndTime < req.EndTime) ||
                (dlt.StartTime > req.StartTime && dlt.EndTime < req.EndTime)
            );
        if (checkTime != null)
            throw new BadRequestException("Invalid log time");

        DoctorLogTime entity = Mapper.Map(req, new DoctorLogTime());
        entity.DoctorId = CurrentUserID;
        await _doctorLogTimeRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [Authorize(Roles = PolicyName.DOCTOR)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorLogTime(Guid id, [FromBody] UpdateDoctorLogTimeRequest req)
    {
        var target = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        DoctorLogTime entity = Mapper.Map(req, target);
        await _doctorLogTimeRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [Authorize(Roles = PolicyName.DOCTOR)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorInfo(Guid id)
    {
        var target = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Soft Delete?
        await _doctorLogTimeRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

}
