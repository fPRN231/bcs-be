using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/medicalhistories")]
public class MedicalHistoriesController : Controller
{
    private readonly IRepositoryBase<MedicalHistory> _medicalHistoryRepository;
    public MedicalHistoriesController(IRepositoryBase<MedicalHistory> medicalHistoryRepository)
    {
        _medicalHistoryRepository = medicalHistoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMedicalHistories()
    {
        return Ok(await _medicalHistoryRepository.ToListAsync());
    }
}
