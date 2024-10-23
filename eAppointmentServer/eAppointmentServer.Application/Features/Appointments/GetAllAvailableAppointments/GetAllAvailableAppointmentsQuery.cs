using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAvailableAppointments;

public sealed record GetAllAvailableAppointmentsQuery(Guid DoctorId)
  : IRequest<Result<List<GetAllAvailableAppointmentsQueryResponse>>>;