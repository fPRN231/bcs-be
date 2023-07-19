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

[Authorize(Roles = PolicyName.DOCTOR)]
[Route("/v1/bcs/doctor-logtimes")]
public class DoctorLogTimesController : BaseController
{
    private readonly IRepositoryBase<DoctorLogTime> _doctorLogTimeRepository;
    private readonly IRepositoryBase<User> _userRepository;

    public DoctorLogTimesController(IRepositoryBase<DoctorLogTime> doctorLogTimeRepository, IRepositoryBase<User> userRepository)
    {
        _doctorLogTimeRepository = doctorLogTimeRepository;
        _userRepository = userRepository;
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

    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetAvailableLogTimes(Guid doctorId)
    {
        IOrderedEnumerable<DoctorLogTime> availableLogTimes;
        availableLogTimes = (await _doctorLogTimeRepository.WhereAsync(x => x.DoctorId.Equals(doctorId)))
                                                           .OrderBy(c => c.StartTime);
        return Ok(availableLogTimes);
    }

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
        entity.CreatedAt = DateTime.Now;
        await _doctorLogTimeRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorLogTime(Guid id, [FromBody] UpdateDoctorLogTimeRequest req)
    {
        var target = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        DoctorLogTime entity = Mapper.Map(req, target);
        entity.ModifiedAt = DateTime.Now;
        await _doctorLogTimeRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorInfo(Guid id)
    {
        var target = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Soft Delete?
        await _doctorLogTimeRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

}
