using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/doctorLogTimes")]
public class DoctorLogTimesController : BaseController
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
