using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/medicalhistories")]
public class MedicalHistoriesController : BaseController
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
