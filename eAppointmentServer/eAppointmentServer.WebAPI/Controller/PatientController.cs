using eAppointmentServer.Application.Features.Patients.CreatePatient;
using eAppointmentServer.Application.Features.Patients.DeletePatient;
using eAppointmentServer.Application.Features.Patients.GetAllPatients;
using eAppointmentServer.Application.Features.Patients.GetPatientByPatientId;
using eAppointmentServer.Application.Features.Patients.UpdatePatient;
using eAppointmentServer.WebAPI.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controller;

public sealed class PatientController(IMediator mediator) : ApiController(mediator)
{
  [HttpPost]
  public async Task<IActionResult> GetAll(GetAllPatientsQuery request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Create(CreatePatientCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Update(UpdatePatientCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> Delete(DeletePatientCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> GetPatientByPatientIdentityNumber(GetPatientByPatientIdentityNumberQuery request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
}