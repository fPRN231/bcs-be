using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/services")]
public class DoctorLogTimesController : Controller
{
    private readonly IRepositoryBase<DoctorLogTime> _doctorLogTimeController;

    public DoctorLogTimesController(IRepositoryBase<DoctorLogTime> doctorLogTimeController)
    {
        _doctorLogTimeController = doctorLogTimeController;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _doctorLogTimeController.ToListAsync());
    }
}
