using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointment;

internal sealed class DeleteAppointmentCommandHandler(
  IAppointmentRepository repository,
  IUnitOfWork unitOfWork): IRequestHandler<DeleteAppointmentCommand, Result<string>>
{
  public async Task<Result<string>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
  {
    Appointment appointment = await repository.Where(p => p.Id == request.Id).FirstAsync(cancellationToken);

    if (appointment is null)
    {
      return Result<string>.Failure("No Record");
    }
    
    repository.Delete(appointment);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return "Appointment is Deleted";
  }
}