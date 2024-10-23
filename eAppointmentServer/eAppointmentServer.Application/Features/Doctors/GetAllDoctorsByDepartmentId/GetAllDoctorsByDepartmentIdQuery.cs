using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctorsByDepartmentId;

public sealed record GetAllDoctorsByDepartmentIdQuery(int DepartmentValue): IRequest<Result<List<Doctor>>>;