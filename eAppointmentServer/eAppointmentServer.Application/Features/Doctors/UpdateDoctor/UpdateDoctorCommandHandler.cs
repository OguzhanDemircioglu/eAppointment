using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.UpdateDoctor;

internal sealed class UpdateDoctorCommandHandler(
  IDoctorRepository repository,
  IMapper mapper,
  IUnitOfWork unitOfWork) : IRequestHandler<UpdateDoctorCommand, Result<string>>
{
  public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
  {
    Doctor doctor = await repository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

    if (doctor is null)
    {
      return Result<string>.Failure("No Record");
    }
    
    doctor = mapper.Map<Doctor>(request);
    
    repository.Update(doctor);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return "Doctor is Updated";
  }
}