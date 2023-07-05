using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/services")]
public class AppointmentsController : Controller
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
