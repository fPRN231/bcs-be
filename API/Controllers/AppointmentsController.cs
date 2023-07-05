using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/appointments")]
public class AppointmentsController : BaseController
{
    private readonly IRepositoryBase<Appointment> _appointmentRepostory;

    public AppointmentsController(IRepositoryBase<Appointment> appointmentRepostory)
    {
        _appointmentRepostory = appointmentRepostory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        return Ok(await _appointmentRepostory.ToListAsync());
    }
}
