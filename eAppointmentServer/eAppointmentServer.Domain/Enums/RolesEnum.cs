using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Domain.Enums;

public class RolesEnum
{
  public static List<AppRole> GetRoles()
  {
    List<string> roles = new()
    {
      "Admin",
      "Doctor",
      "Personal"
    };

    return roles.Select(s => new AppRole() { Name = s }).ToList();
  }
}