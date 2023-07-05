using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/services")]
public class DoctorInfosController : Controller
{
    private readonly IRepositoryBase<Service> _doctorInfoRepository;

    public DoctorInfosController(IRepositoryBase<Service> doctorInfoRepository)
    {
        _doctorInfoRepository = doctorInfoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctorInfos()
    {
        return Ok(await _doctorInfoRepository.ToListAsync());
    }
}
