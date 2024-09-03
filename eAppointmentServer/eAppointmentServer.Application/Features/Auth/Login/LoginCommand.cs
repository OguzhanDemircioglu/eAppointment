using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Auth.Login;

public sealed record LoginCommand(
    string UsernameOrEmail,
    string Password): IRequest<Result<LoginCommandResponse>>;