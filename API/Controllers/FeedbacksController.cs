using API.Models.Request.Feedbacks;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
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

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateFeedback(Guid appointmentId, [FromBody] CreateFeedbackRequest req)
    {
        Feedback entity = Mapper.Map(req, new Feedback());
        entity.AppointmentId = appointmentId;
        entity.UserId = CurrentUserID;
        await _feedbackRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeedback(Guid id, [FromBody] UpdateFeedbackRequest req)
    {
        var target = await _feedbackRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Feedback entity = Mapper.Map(req, target);
        await _feedbackRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(Guid id)
    {
        var target = await _feedbackRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        await _feedbackRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }


}
