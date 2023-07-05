using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/services")]
public class QualificationsController : Controller
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
