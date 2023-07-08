using API.Models.Request;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("/v1/bcs/appointments")]
public class AppointmentsController : BaseController
{
    private readonly IRepositoryBase<Appointment> _appointmentRepostory;
    private readonly IRepositoryBase<DoctorLogTime> _doctorLogTimeRepository;

    public AppointmentsController(IRepositoryBase<Appointment> appointmentRepostory, IRepositoryBase<DoctorLogTime> doctorLogTimeRepository)
    {
        _appointmentRepostory = appointmentRepostory;
        _doctorLogTimeRepository = doctorLogTimeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        return Ok(await _appointmentRepostory.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest req)
    {
        Appointment entity = Mapper.Map(req, new Appointment());
        await _appointmentRepostory.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentRequest req)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Appointment entity = Mapper.Map(req, target);
        await _appointmentRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _appointmentRepostory.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
