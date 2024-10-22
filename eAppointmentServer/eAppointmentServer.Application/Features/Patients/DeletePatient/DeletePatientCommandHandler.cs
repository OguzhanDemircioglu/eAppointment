using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.DeletePatient;

internal sealed class DeletePatientCommandHandler(
  IPatientRepository repository,
  IUnitOfWork unitOfWork): IRequestHandler<DeletePatientCommand, Result<string>>
{
  public async Task<Result<string>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
  {
    Patient patient = await repository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

    if (patient is null)
    {
      return Result<string>.Failure("No Record");
    }
    
    repository.Delete(patient);
    await unitOfWork.SaveChangesAsync(cancellationToken);
    
    return "Patient is Deleted";
  }
}