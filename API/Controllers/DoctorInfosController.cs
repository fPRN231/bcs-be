using API.Models.Request.DoctorInfos;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("/v1/bcs/doctorInfos")]
public class DoctorInfosController : BaseController
{
    private readonly IRepositoryBase<DoctorInfo> _doctorInfoRepository;
    private readonly IRepositoryBase<User> _userRepository;

    public DoctorInfosController(IRepositoryBase<DoctorInfo> doctorInfoRepository, IRepositoryBase<User> userRepository)
    {
        _doctorInfoRepository = doctorInfoRepository;
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorInfo(Guid id)
    {
        var target = await _doctorInfoRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [Authorize(Roles = PolicyName.DOCTOR)]
    [HttpPost]
    public async Task<IActionResult> CreateDoctorInfo([FromBody] CreateDoctorInfoRequest req)
    {
        DoctorInfo entity = Mapper.Map(req, new DoctorInfo());
        entity.DoctorId = CurrentUserID;
        await _doctorInfoRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [Authorize(Roles = PolicyName.DOCTOR)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctorInfo(Guid id, [FromBody] UpdateDoctorInfoRequest req)
    {
        var target = await _doctorInfoRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        DoctorInfo entity = Mapper.Map(req, target);
        await _doctorInfoRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [Authorize(Roles = PolicyName.DOCTOR)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorInfo(Guid id)
    {
        var target = await _doctorInfoRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Soft Delete
        await _doctorInfoRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }

   

    




}
