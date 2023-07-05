using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/qualifications")]
public class QualificationsController : BaseController
{
    private readonly IRepositoryBase<Qualification> _qualificationController;

    public QualificationsController(IRepositoryBase<Qualification> qualificationController)
    {
        _qualificationController = qualificationController;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _qualificationController.ToListAsync());
    }
}
