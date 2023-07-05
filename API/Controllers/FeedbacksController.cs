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
    public async Task<IActionResult> GetFeedbackOfAppointment(Guid appointmentId)
    {
        return Ok(await _feedbackRepository.WhereAsync(x => x.AppointmentId.Equals(appointmentId)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeedback(Guid id)
    {
        var target = await _feedbackRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }




}
