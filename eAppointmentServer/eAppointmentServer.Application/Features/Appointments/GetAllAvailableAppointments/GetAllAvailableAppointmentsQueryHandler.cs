using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAvailableAppointments;

internal sealed class GetAllAvailableAppointmentsQueryHandler(IAppointmentRepository repository)
  : IRequestHandler<GetAllAvailableAppointmentsQuery, Result<List<GetAllAvailableAppointmentsQueryResponse>>>
{
  public async Task<Result<List<GetAllAvailableAppointmentsQueryResponse>>> Handle(GetAllAvailableAppointmentsQuery request, CancellationToken cancellationToken)
  {
    var appointments = await repository
      .Where(p => p.DoctorId == request.DoctorId)
      .Include(p=> p.Patient)
      !.ToListAsync(cancellationToken);
      
    List<GetAllAvailableAppointmentsQueryResponse> response = 
      appointments.Select(s => 
          new GetAllAvailableAppointmentsQueryResponse(
            s.Id,
            s.StartDate, 
            s.EndDate, 
            s.Patient!.FullName,
            s.Patient))
        .ToList();

    return response;
  }
}