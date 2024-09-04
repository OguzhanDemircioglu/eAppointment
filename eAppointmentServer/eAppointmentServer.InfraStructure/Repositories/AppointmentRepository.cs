using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.InfraStructure.Context;
using GenericRepository;

namespace eAppointmentServer.InfraStructure.Repositories;

internal sealed class AppointmentRepository(ApplicationDbContext context)
    : Repository<Appointment, ApplicationDbContext>(context), IAppointmentRepository;