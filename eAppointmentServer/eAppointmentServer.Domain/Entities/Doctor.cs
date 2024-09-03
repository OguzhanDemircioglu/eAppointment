using eAppointmentServer.Domain.Abstractions;
using eAppointmentServer.Domain.Enums;

namespace eAppointmentServer.Domain.Entities;

public sealed class Doctor: Entity
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);
    public DepartmentEnum Department { get; set; } = DepartmentEnum.Acil;
}