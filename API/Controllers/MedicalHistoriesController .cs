using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/services")]
public class MedicalHistoriesController : Controller
{
    private readonly IRepositoryBase<MedicalHistory> _medicalHistoryController;

    public MedicalHistoriesController(IRepositoryBase<MedicalHistory> medicalHistoryController)
    {
        _medicalHistoryController = medicalHistoryController;
    }

    [HttpGet]
    public async Task<IActionResult> GetMedicalHistories()
    {
        return Ok(await _medicalHistoryController.ToListAsync());
    }
}
