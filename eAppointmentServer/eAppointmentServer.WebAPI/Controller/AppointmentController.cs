using eAppointmentServer.Application.Features.Appointments.CreateAppointment;
using eAppointmentServer.Application.Features.Appointments.DeleteAppointment;
using eAppointmentServer.Application.Features.Appointments.GetAllAvailableAppointments;
using eAppointmentServer.Application.Features.Appointments.UpdateAppointment;
using eAppointmentServer.WebAPI.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controller;

public sealed class AppointmentController(IMediator mediator) : ApiController(mediator)
{
  [HttpPost]
  public async Task<IActionResult> GetAllAvailableAppointments(GetAllAvailableAppointmentsQuery request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Create(CreateAppointmentCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Delete(DeleteAppointmentCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Update(UpdateAppointmentCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
}