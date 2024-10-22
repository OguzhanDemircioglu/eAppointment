using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients;

internal sealed class GetAllPatientsQueryHandler(
  IPatientRepository repository) : IRequestHandler<GetAllPatientsQuery, Result<List<Patient>>>
{
  public async Task<Result<List<Patient>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
  {
    var patients = await repository
      .GetAll()
      .OrderBy(o => o.FirstName)
      .ToListAsync(cancellationToken);

    return patients;
  }
}