using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/service")]
public class ServicesController : Controller
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
}
