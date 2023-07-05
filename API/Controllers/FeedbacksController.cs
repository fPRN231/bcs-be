using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/feedbacks")]
public class FeedbacksController : BaseController
{
    private readonly IRepositoryBase<Feedback> _feedbackController;

    public FeedbacksController(IRepositoryBase<Feedback> feedbackController)
    {
        _feedbackController = feedbackController;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await _feedbackController.ToListAsync());
    }
}
