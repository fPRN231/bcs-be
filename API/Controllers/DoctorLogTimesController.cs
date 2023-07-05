using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/doctorLogTimes")]
public class DoctorLogTimesController : BaseController
{
    private readonly IRepositoryBase<DoctorLogTime> _doctorLogTimeRepository;

    public DoctorLogTimesController(IRepositoryBase<DoctorLogTime> doctorLogTimeRepository)
    {
        _doctorLogTimeRepository = doctorLogTimeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _doctorLogTimeRepository.ToListAsync());
    }
}
