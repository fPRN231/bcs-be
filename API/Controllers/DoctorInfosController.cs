using API.Models;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/doctorInfos")]
public class DoctorInfosController : BaseController
{
    private readonly IRepositoryBase<DoctorInfo> _doctorInfoRepository;

    public DoctorInfosController(IRepositoryBase<DoctorInfo> doctorInfoRepository)
    {
        _doctorInfoRepository = doctorInfoRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorInfo(Guid id)
    {
        var target = await _doctorInfoRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctorInfo([FromBody] CreateDoctorInfoRequest req)
    {
        DoctorInfo entity = Mapper.Map(req, new DoctorInfo());
        entity.DoctorId = CurrentUserID;
        await _doctorInfoRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorInfo(Guid id, [FromBody] UpdateDoctorInfoRequest req)
    {
        var target = await _doctorInfoRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        DoctorInfo entity = Mapper.Map(req, target);
        await _doctorInfoRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorInfo(Guid id)
    {
        var target = await _doctorInfoRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _doctorInfoRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

   

    




}
