using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAvailableAppointments;

public sealed record GetAllAvailableAppointmentsQueryResponse(
  Guid Id,
  DateTime StartDate,
  DateTime EndDate,
  string Title,
  Patient Patient
);