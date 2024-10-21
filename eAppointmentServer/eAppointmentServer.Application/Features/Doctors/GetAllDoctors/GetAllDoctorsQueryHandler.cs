using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctors;

internal sealed class GetAllDoctorsQueryHandler(
  IDoctorRepository repository) : IRequestHandler<GetAllDoctorsQuery, Result<List<Doctor>>>
{
  public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
  {
    var doctors = await repository.GetAll()
      .OrderBy(p => p.Department)
      .ThenBy(p => p.FirstName)
      .ToListAsync(cancellationToken);

    return doctors;
  }
}