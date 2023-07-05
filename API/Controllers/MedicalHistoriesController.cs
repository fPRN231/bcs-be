using API.Models;
using API.Models.Request;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("/v1/bcs/medicalhistories")]
public class MedicalHistoriesController : BaseController
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicalHistory(Guid id)
    {
        var target = await _medicalHistoryRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedicalHistory([FromBody] CreateMedicalHistoryRequest req)
    {
        MedicalHistory entity = Mapper.Map(req, new MedicalHistory());
        await _medicalHistoryRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorInfo(Guid id, [FromBody] UpdateMedicalHistoryRequest req)
    {
        var target = await _medicalHistoryRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        MedicalHistory entity = Mapper.Map(req, target);
        await _medicalHistoryRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorInfo(Guid id)
    {
        var target = await _medicalHistoryRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _medicalHistoryRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
