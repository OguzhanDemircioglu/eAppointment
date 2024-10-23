using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.GetPatientByPatientId;

internal sealed class GetPatientByPatientIdentityNumberQueryHandler(IPatientRepository repository): 
  IRequestHandler<GetPatientByPatientIdentityNumberQuery, Result<Patient>>
{
  public async Task<Result<Patient>> Handle(GetPatientByPatientIdentityNumberQuery request, CancellationToken cancellationToken)
  {
    Patient? patient = 
      await repository
        .GetByExpressionAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);

    return patient;
  }
}