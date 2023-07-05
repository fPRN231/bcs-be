using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/medicalhistory")]
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
