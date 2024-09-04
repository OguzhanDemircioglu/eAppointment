using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.InfraStructure.Context;
using GenericRepository;

namespace eAppointmentServer.InfraStructure.Repositories;

internal sealed class UserRoleRepository(ApplicationDbContext context)
    : Repository<AppUserRole, ApplicationDbContext>(context), IUserRoleRepository;