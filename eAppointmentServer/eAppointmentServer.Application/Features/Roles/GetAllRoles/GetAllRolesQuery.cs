using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.GetAllRoles;

public record GetAllRolesQuery(): IRequest<Result<List<AppRole>>>;