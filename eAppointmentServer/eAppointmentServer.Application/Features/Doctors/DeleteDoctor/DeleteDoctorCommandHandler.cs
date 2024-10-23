using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctor;

internal sealed class DeleteDoctorCommandHandler(
  IDoctorRepository repository,
  IUnitOfWork unitOfWork) : IRequestHandler<DeleteDoctorCommand, Result<string>>
{
  public async Task<Result<string>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
  {
    Doctor doctor = await repository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

    if (doctor is null)
    {
      return Result<string>.Failure(ResultMessages.NO_RECORD);
    }

    repository.Delete(doctor);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return ResultMessages.RECORD_DELETED;
  }
}