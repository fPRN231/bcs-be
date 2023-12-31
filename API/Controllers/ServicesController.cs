﻿using API.Models.Request.Services;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/services")]
public class ServicesController : BaseController
{
    private readonly IRepositoryBase<Service> _serviceRepostory;

    public ServicesController(IRepositoryBase<Service> serviceRepostory)
    {
        _serviceRepostory = serviceRepostory;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _serviceRepostory.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetService(Guid id)
    {
        var target = await _serviceRepostory.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceRequest req)
    {
        Service entity = Mapper.Map(req, new Service());
        await _serviceRepostory.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(Guid id, [FromBody] UpdateServiceRequest req)
    {
        var target = await _serviceRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Service entity = Mapper.Map(req, target);
        await _serviceRepostory.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(Guid id)
    {
        var target = await _serviceRepostory.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Soft Delete
        await _serviceRepostory.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
