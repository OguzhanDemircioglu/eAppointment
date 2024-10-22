using eAppointmentServer.Domain.Entities;
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
      return Result<string>.Failure("No Record");
    }
    
    repository.Delete(doctor);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return "Doctor is Deleted";
  }
}