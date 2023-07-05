using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/doctorInfo")]
public class DoctorInfosController : BaseController
{
    private readonly IRepositoryBase<DoctorInfo> _doctorInfoRepository;

    public DoctorInfosController(IRepositoryBase<DoctorInfo> doctorInfoRepository)
    {
        _doctorInfoRepository = doctorInfoRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorInfo(string id)
    {
        var target = await _doctorInfoRepository.FirstOrDefaultAsync(x => x.DoctorId.Equals(id));
        return Ok(target);
    }




}
