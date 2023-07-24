using API.Models.Request.Appointments;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers;

[Authorize]
[Route("/v1/bcs/appointments")]
public class AppointmentsController : BaseController
{
    private readonly IRepositoryBase<Appointment> _appointmentRepostory;
    private readonly IRepositoryBase<User> _userRepository;
    private readonly IRepositoryBase<DoctorLogTime> _doctorLogTimeRepository;
    private readonly IRepositoryBase<Service> _serviceRepository;

    public AppointmentsController(IRepositoryBase<Appointment> appointmentRepostory, IRepositoryBase<User> userRepository, IRepositoryBase<DoctorLogTime> doctorLogTimeRepository, IRepositoryBase<Service> serviceRepository)
    {
        _appointmentRepostory = appointmentRepostory;
        _userRepository = userRepository;
        _doctorLogTimeRepository = doctorLogTimeRepository;
        _serviceRepository = serviceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        return Ok(await _appointmentRepostory.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var target = await _appointmentRepostory.FirstOrDefaultAsync(x => x.Id.Equals(id), "Services");
        return Ok(target);
    }

    //[HttpPost("booking/{Id}")]
    //public async Task<IActionResult> SelectAppointmentInformation([FromBody] CreateAppointmentRequest req)
    //{
    //    Appointment entity = Mapper.Map(req, new Appointment());
    //    var doctorLogTime = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(req.DoctorLogTimeId), new NotFoundException());
    //    if (!doctorLogTime.IsAvailable)
    //    {
    //        throw new BadRequestException("Unavailable");
    //    }
    //    entity.StartDateTime = CreateDayOfWeek(doctorLogTime.DayOfWeek, (int)doctorLogTime.StartTime % 23);
    //    entity.EndDateTime = CreateDayOfWeek(doctorLogTime.DayOfWeek, (int)doctorLogTime.EndTime % 23);
    //    entity.AppointmentStatus = AppointmentStatus.Draft;
    //    await _appointmentRepostory.UpdateAsync(entity);
    //    return StatusCode(StatusCodes.Status204NoContent);
    //}

    //[HttpPatch("booking/complete-form")]
    //public async Task<IActionResult> CompleteAppointmentForm(Guid Id, [FromBody] CompleteAppointmentForm req)
    //{
    //    var target = await _appointmentRepostory.FoundOrThrow(c => c.Id.Equals(Id), new NotFoundException());
    //    Appointment entity = Mapper.Map(req, target);
    //    entity.AppointmentStatus = AppointmentStatus.Pending;
    //    entity.CreatedAt = DateTime.Now;
    //    await _appointmentRepostory.UpdateAsync(entity);
    //    return StatusCode(StatusCodes.Status204NoContent);
    //}

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest req)
    {
        Appointment entity = Mapper.Map(req, new Appointment());
        var doctorLogTime = await _doctorLogTimeRepository.FoundOrThrow(c => c.Id.Equals(req.DoctorLogTimeId), new NotFoundException());
        if (!doctorLogTime.IsAvailable)
        {
            throw new BadRequestException("Unavailable");
        }

        if (req.ServicesList.Count > 0)
        {
            foreach (var serviceId in req.ServicesList)
            {
                var service = await _serviceRepository.FirstOrDefaultAsync(s => s.Id.Equals(serviceId));
                entity.Services.Add(service);
            }
        }
        else
        {
            throw new BadRequestException("Must select at least 1 service!");
        }

        entity.StartDateTime = CreateDayOfWeek(doctorLogTime.DayOfWeek, (int)doctorLogTime.StartTime % 23);
        entity.EndDateTime = CreateDayOfWeek(doctorLogTime.DayOfWeek, (int)doctorLogTime.EndTime % 23);
        entity.AppointmentStatus = AppointmentStatus.Pending;
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

    private DateTime CreateDayOfWeek(DayOfWeek dayOfWeek, int hour, int min = 0)
    {
        DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, min, 0);


        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysUntil = ((int)dayOfWeek - (int)dt.DayOfWeek + 7) % 7;
        //  DateTime nextTuesday = today.AddDays(daysUntilTuesday);

        dt = dt.AddDays(daysUntil);

        return dt;
    }

}
