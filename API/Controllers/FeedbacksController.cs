using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/feedbacks")]
public class FeedbacksController : BaseController
{
    private readonly IRepositoryBase<Feedback> _feedbackRepository;

    public FeedbacksController(IRepositoryBase<Feedback> feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _feedbackRepository.ToListAsync());
    }
}
