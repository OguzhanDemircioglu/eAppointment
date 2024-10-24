using eAppointmentServer.Application.Features.Users.CreateUser;
using eAppointmentServer.Application.Features.Users.DeleteUser;
using eAppointmentServer.Application.Features.Users.GetAllUsers;
using eAppointmentServer.Application.Features.Users.UpdateUser;
using eAppointmentServer.WebAPI.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controller;

public sealed class UserController(IMediator mediator) : ApiController(mediator)
{
  [HttpPost]
  public async Task<IActionResult> GetAll(GetAllUsersQuery request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }

  [HttpPost]
  public async Task<IActionResult> DeleteById(DeleteUserByIdCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }

  [HttpPost]
  public async Task<IActionResult> Update(UpdateUserCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);
    return StatusCode(response.StatusCode, response);
  }
}