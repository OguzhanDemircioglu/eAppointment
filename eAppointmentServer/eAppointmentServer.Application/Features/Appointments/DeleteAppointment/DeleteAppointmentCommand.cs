using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointment;

public sealed record DeleteAppointmentCommand(Guid Id): IRequest<Result<string>>;