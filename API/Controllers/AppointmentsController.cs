using API.Models.Request.Appointments;
using Domain.Constants;
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
    private readonly IRepositoryBase<User> _userRepository;

    public AppointmentsController(IRepositoryBase<Appointment> appointmentRepostory, IRepositoryBase<DoctorLogTime> doctorLogTimeRepostory, IRepositoryBase<User> userRepository)
    {
        _appointmentRepostory = appointmentRepostory;
        _userRepository = userRepository;
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

    [HttpPost("booking/select-doctor")]
    public async Task<IActionResult> SelectAppointmentDoctor([FromBody] SelectAppointmentDoctor req)
    {
        Appointment entity = Mapper.Map(req, new Appointment());
        entity.AppointmentStatus = AppointmentStatus.Draft;
        await _appointmentRepostory.CreateAsync(entity);
        return Ok(entity);
    }

    [HttpPatch("booking/{Id}/select-time-services")]
    public async Task<IActionResult> SelectAppointmentTimeAndServices(Guid Id, [FromBody] SelectAppointmentTimeAndServices req)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(Id), new NotFoundException());
        Appointment entity = Mapper.Map(req, target);
        await _appointmentRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPatch("booking/complete-form")]
    public async Task<IActionResult> CompleteAppointmentForm(Guid Id, [FromBody] CompleteAppointmentForm req)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(Id), new NotFoundException());
        Appointment entity = Mapper.Map(req, target);
        entity.AppointmentStatus = AppointmentStatus.Pending;
        entity.CreatedAt = DateTime.Now;
        await _appointmentRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest req)
    {
        Appointment entity = Mapper.Map(req, new Appointment());
        entity.AppointmentStatus = AppointmentStatus.Pending;
        entity.CreatedAt = DateTime.Now;
        await _appointmentRepostory.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentRequest req)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Appointment entity = Mapper.Map(req, target);
        entity.ModifiedAt = DateTime.Now;
        await _appointmentRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Add soft delete?
        await _appointmentRepostory.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPatch("{id}/confirmed")]
    public async Task<IActionResult> ConfirmAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        target.AppointmentStatus = AppointmentStatus.Confirmed;
        await _appointmentRepostory.UpdateAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPatch("{id}/completed")]
    public async Task<IActionResult> CompleteAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        target.AppointmentStatus = AppointmentStatus.Completed;
        await _appointmentRepostory.UpdateAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPatch("{id}/cancelled")]
    public async Task<IActionResult> CancelAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        target.AppointmentStatus = AppointmentStatus.Cancelled;
        await _appointmentRepostory.UpdateAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    
}
