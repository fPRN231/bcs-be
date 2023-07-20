using API.Models.Request.Feedbacks;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("/v1/bcs/feedbacks")]
public class FeedbacksController : BaseController
{
    private readonly IRepositoryBase<Feedback> _feedbackRepository;
    private readonly IRepositoryBase<User> _userRepository;
    private readonly IRepositoryBase<Appointment> _appointmentsRepository;

    public FeedbacksController(IRepositoryBase<Feedback> feedbackRepository, IRepositoryBase<User> userRepository, IRepositoryBase<Appointment> appointmentsRepository)
    {
        _feedbackRepository = feedbackRepository;
        _userRepository = userRepository;
        _appointmentsRepository = appointmentsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetFeedbacksOfAppointment(Guid appointmentId)
    {
        var target = await _feedbackRepository.WhereAsync(x => x.AppointmentId.Equals(appointmentId));
        return Ok(target);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeedback(Guid id)
    {
        var target = await _feedbackRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Ok(target);
    }

    [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> CreateFeedback(Guid id, [FromBody] CreateFeedbackRequest req)
    {
        Feedback entity = Mapper.Map(req, new Feedback());
        entity.AppointmentId = id;
        entity.UserId = CurrentUserID;
        await _feedbackRepository.CreateAsync(entity);
        return StatusCode(StatusCodes.Status201Created);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeedback(Guid id, [FromBody] UpdateFeedbackRequest req)
    {
        var target = await _feedbackRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        Feedback entity = Mapper.Map(req, target);
        await _feedbackRepository.UpdateAsync(entity);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(Guid id)
    {
        var target = await _feedbackRepository.FoundOrThrow(c => c.Id.Equals(id), new NotFoundException());
        //Soft Delete
        await _feedbackRepository.DeleteAsync(target);
        return StatusCode(StatusCodes.Status204NoContent);
    }


}
