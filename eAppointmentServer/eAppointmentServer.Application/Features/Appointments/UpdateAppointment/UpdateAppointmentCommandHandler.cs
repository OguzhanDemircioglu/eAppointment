using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.UpdateAppointment;

internal sealed class UpdateAppointmentCommandHandler(
  IAppointmentRepository repository,
  IUnitOfWork unitOfWork) : IRequestHandler<UpdateAppointmentCommand, Result<string>>
{
  public async Task<Result<string>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
  {
    Appointment appointment = await repository.Where(p => p.Id == request.Id).FirstAsync(cancellationToken);

    if (appointment is null)
    {
      return Result<string>.Failure("No Record");
    }

    DateTime startDate = Convert.ToDateTime(request.StartDate);
    DateTime endDate = Convert.ToDateTime(request.EndDate);

    bool isAppointmentDateNotAvailable =
      await repository
        .AnyAsync(p => p.DoctorId == appointment.DoctorId && p.Id != request.Id &&
                       ((p.StartDate < endDate && p.StartDate >= startDate) ||
                        (p.EndDate > startDate && p.EndDate <= endDate) ||
                        (p.StartDate >= startDate && p.EndDate <= endDate) ||
                        (p.StartDate <= startDate && p.EndDate >= endDate)),
          cancellationToken);

    if (isAppointmentDateNotAvailable)
    {
      return Result<string>.Failure("Appointment date is not available");
    }
    
    bool isPatientHasConflictingAppointmentWithAnotherDoctor =
      await repository
        .AnyAsync(p => p.PatientId == appointment.PatientId &&
                       p.DoctorId != appointment.DoctorId &&
                       ((p.StartDate < endDate && p.StartDate >= startDate) ||
                         (p.EndDate > startDate && p.EndDate <= endDate) ||
                         (p.StartDate >= startDate && p.EndDate <= endDate) || 
                         (p.StartDate <= startDate && p.EndDate >= endDate)
                       ), cancellationToken);
    
    if (isPatientHasConflictingAppointmentWithAnotherDoctor)
    {
      return Result<string>.Failure("Patient already has an appointment with another doctor at the same time");
    }

    appointment.StartDate = startDate;
    appointment.EndDate = endDate;

    repository.Update(appointment);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return "Appointment is Updated";
  }
}