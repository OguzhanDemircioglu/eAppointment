using AutoMapper;
using eAppointmentServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;

namespace eAppointmentServer.Application.Mapping;

public sealed class MappingProfile: Profile
{
  public MappingProfile()
  {
    CreateMap<CreateDoctorCommand, Doctor>()
      .ForMember(m=>m.Department, options =>
      {
        options.MapFrom(t=> DepartmentEnum.FromValue(t.DepartmentValue));
      });
  }
}