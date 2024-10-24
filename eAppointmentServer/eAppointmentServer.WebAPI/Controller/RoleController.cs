using eAppointmentServer.Application.Features.Roles.GetAllRoles;
using eAppointmentServer.Application.Features.Roles.RoleSync;
using eAppointmentServer.WebAPI.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controller;

public sealed class RoleController(IMediator mediator) : ApiController(mediator)
{
  [HttpPost]
  public async Task<IActionResult> RoleSync(RoleSyncCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
  
  [HttpPost]
  public async Task<IActionResult> GetAllRoles(GetAllRolesQuery request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
}