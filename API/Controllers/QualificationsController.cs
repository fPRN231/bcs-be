using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/qualifications")]
public class QualificationsController : Controller
{
    private readonly IRepositoryBase<Qualification> _qualificationRepository;

    public QualificationsController(IRepositoryBase<Qualification> qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _qualificationRepository.ToListAsync());
    }
}
