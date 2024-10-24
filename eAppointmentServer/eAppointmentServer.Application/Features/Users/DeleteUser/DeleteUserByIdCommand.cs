using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.DeleteUser;

public sealed record DeleteUserByIdCommand(
  Guid Id) : IRequest<Result<string>>;