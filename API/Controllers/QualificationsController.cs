using API.Models.Request.Qualifications;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("/v1/bcs/qualifications")]
public class QualificationsController : BaseController
{
    private readonly IRepositoryBase<Qualification> _qualificationRepository;

    public QualificationsController(IRepositoryBase<Qualification> qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }


    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetQualifications(Guid doctorId)
    {
        return Ok(await _qualificationRepository.WhereAsync(x => x.DoctorId.Equals(doctorId)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateQualification([FromBody] CreateQualificationRequest req)
    {
        Qualification entity = Mapper.Map(req, new Qualification());
        entity.DoctorId = CurrentUserID;
        await _qualificationRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQualification(Guid id, [FromBody] UpdateQualificationRequest req)
    {
        var target = await _qualificationRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Qualification entity = Mapper.Map(req, target);
        await _qualificationRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQualification(Guid id)
    {
        var target = await _qualificationRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _qualificationRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
