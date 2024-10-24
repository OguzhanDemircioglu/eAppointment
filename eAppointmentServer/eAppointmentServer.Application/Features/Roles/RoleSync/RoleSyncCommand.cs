using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.RoleSync;

public sealed record RoleSyncCommand(): IRequest<Result<string>>;

internal sealed class RoleSyncCommandHandler(
  RoleManager<AppRole> roleManager): IRequestHandler<RoleSyncCommand, Result<string>>
{
  public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
  {
    List<AppRole> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

    List<AppRole> staticRoles = RolesEnum.GetRoles();

    foreach (var role in currentRoles)
    {
      if (staticRoles.All(p => p.Name != role.Name))
      {
        await roleManager.DeleteAsync(role);
      }
    }

    foreach (var role in staticRoles)
    {
      if (currentRoles.All(p => p.Name != role.Name))
      {
        await roleManager.CreateAsync(new (){Name = role.Name, ConcurrencyStamp = DateTime.Now+""});
      }
    }

    return ResultMessages.SYNC_ROLES;
  }
}