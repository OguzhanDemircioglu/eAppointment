﻿using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.CreatePatient;

internal sealed class CreatePatientCommandHandler(
  IPatientRepository repository,
  IMapper mapper,
  IUnitOfWork unitOfWork) : IRequestHandler<CreatePatientCommand, Result<string>>
{
  public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
  {
    if (repository.Any(p => p.IdentityNumber == request.IdentityNumber))
    {
      return Result<string>.Failure(ResultMessages.SAME_IDENTITY);
    }

    Patient patient = mapper.Map<Patient>(request);

    await repository.AddAsync(patient, cancellationToken);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return ResultMessages.RECORD_ADDED;
  }
}