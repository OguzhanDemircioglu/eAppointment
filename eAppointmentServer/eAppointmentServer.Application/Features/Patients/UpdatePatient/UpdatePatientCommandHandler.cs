using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.UpdatePatient;

internal sealed class UpdatePatientCommandHandler(
  IPatientRepository repository,
  IMapper mapper,
  IUnitOfWork unitOfWork) : IRequestHandler<UpdatePatientCommand, Result<string>>
{
  public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
  {
    var patient = await repository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

    if (patient is null)
    {
      return Result<string>.Failure(ResultMessages.NO_RECORD);
    }

    patient = mapper.Map<Patient>(request);

    repository.Update(patient);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return ResultMessages.RECORD_UPDATED;
  }
}