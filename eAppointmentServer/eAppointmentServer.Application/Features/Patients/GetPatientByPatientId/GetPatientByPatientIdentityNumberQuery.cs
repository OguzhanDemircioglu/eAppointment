using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.GetPatientByPatientId;

public sealed record GetPatientByPatientIdentityNumberQuery(string IdentityNumber): IRequest<Result<Patient>>;