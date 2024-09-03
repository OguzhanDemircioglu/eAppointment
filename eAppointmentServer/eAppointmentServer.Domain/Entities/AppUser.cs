using Microsoft.AspNetCore.Identity;

namespace eAppointmentServer.Domain.Entities;

public sealed class AppUser: IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string FullName => string.Join(" ", FirstName, LastName);
}