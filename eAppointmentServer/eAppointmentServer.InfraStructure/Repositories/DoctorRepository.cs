using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.InfraStructure.Context;
using GenericRepository;

namespace eAppointmentServer.InfraStructure.Repositories;

internal sealed class DoctorRepository(ApplicationDbContext context)
    : Repository<Doctor, ApplicationDbContext>(context), IDoctorRepository;
