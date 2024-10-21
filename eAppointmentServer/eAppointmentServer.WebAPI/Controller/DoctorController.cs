using eAppointmentServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentServer.Application.Features.Doctors.GetAllDoctors;
using eAppointmentServer.WebAPI.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controller;

public sealed class DoctorController(IMediator mediator) : ApiController(mediator)
{
  [HttpPost]
  public async Task<IActionResult> GetAll(GetAllDoctorsQuery request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Create(CreateDoctorCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
}