using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(
  IAppointmentRepository repository,
  IPatientRepository patientRepository,
  IUnitOfWork unitOfWork) : IRequestHandler<CreateAppointmentCommand, Result<string>>
{
  public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
  {
    DateTime startDate = Convert.ToDateTime(request.StartDate);
    DateTime endDate = Convert.ToDateTime(request.EndDate);

    Patient patient = new();

    if (request.PatientId is null)
    {
      patient = new Patient
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        IdentityNumber = request.IdentityNumber,
        City = request.City,
        Town = request.Town,
        FullAddress = request.FullAddress
      };

      await patientRepository.AddAsync(patient, cancellationToken);
    }

    bool isAppointmentDateNotAvailable =
      await repository
        .AnyAsync(p => p.DoctorId == request.DoctorId &&
                       ((p.StartDate < endDate &&  p.StartDate >= startDate) ||
                        (p.EndDate > startDate &&  p.EndDate <= endDate) ||
                        (p.StartDate >= startDate && p.EndDate <= endDate) ||
                        (p.StartDate <= startDate && p.EndDate >= endDate)),
          cancellationToken);

    if (isAppointmentDateNotAvailable)
    {
      return Result<string>.Failure(ResultMessages.INVALID_DATE);
    }

    bool isPatientHasConflictingAppointmentWithAnotherDoctor =
      await repository
        .AnyAsync(p => p.PatientId == request.PatientId &&
                       p.DoctorId != request.DoctorId &&
                       ((p.StartDate < endDate && p.StartDate >= startDate) ||
                         (p.EndDate > startDate && p.EndDate <= endDate) ||
                         (p.StartDate >= startDate && p.EndDate <= endDate) ||
                         (p.StartDate <= startDate && p.EndDate >= endDate)
                       ), cancellationToken);

    if (isPatientHasConflictingAppointmentWithAnotherDoctor)
    {
      return Result<string>.Failure(ResultMessages.SAME_DATE);
    }

    Appointment appointment = new()
    {
      DoctorId = request.DoctorId,
      PatientId = request.PatientId ?? patient.Id,
      StartDate = Convert.ToDateTime(request.StartDate),
      EndDate = Convert.ToDateTime(request.EndDate),
      IsComplated = false
    };

    await repository.AddAsync(appointment, cancellationToken);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return ResultMessages.RECORD_ADDED;
  }
}