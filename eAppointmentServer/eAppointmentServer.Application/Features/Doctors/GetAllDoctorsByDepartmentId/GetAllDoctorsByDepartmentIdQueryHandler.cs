using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctorsByDepartmentId;

internal sealed class GetAllDoctorsByDepartmentIdQueryHandler(IDoctorRepository repository): 
  IRequestHandler<GetAllDoctorsByDepartmentIdQuery, Result<List<Doctor>>>
{
  public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsByDepartmentIdQuery request, CancellationToken cancellationToken)
  {
    List<Doctor> doctors = await repository
      .Where(p => p.Department == request.DepartmentValue)
      .OrderBy(p=>p.FirstName)
      .ToListAsync(cancellationToken);

    return doctors;
  }
}