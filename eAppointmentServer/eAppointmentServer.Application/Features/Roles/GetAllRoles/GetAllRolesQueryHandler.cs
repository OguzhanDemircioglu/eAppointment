using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.GetAllRoles;

internal sealed class GetAllRolesQueryHandler(RoleManager<AppRole> roleManager)
  : IRequestHandler<GetAllRolesQuery, Result<List<AppRole>>>
{
  public async Task<Result<List<AppRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
  {
    return await roleManager.Roles.OrderBy(p=> p.Name).ToListAsync(cancellationToken);
  }
}