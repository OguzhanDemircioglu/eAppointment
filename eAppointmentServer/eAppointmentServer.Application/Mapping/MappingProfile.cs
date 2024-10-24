using AutoMapper;
using eAppointmentServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentServer.Application.Features.Patients.CreatePatient;
using eAppointmentServer.Application.Features.Patients.UpdatePatient;
using eAppointmentServer.Application.Features.Users.CreateUser;
using eAppointmentServer.Application.Features.Users.UpdateUser;
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
    
    CreateMap<UpdateDoctorCommand, Doctor>()
      .ForMember(m=>m.Department, options =>
      {
        options.MapFrom(t=> DepartmentEnum.FromValue(t.DepartmentValue));
      });
    
    CreateMap<CreatePatientCommand, Patient>();
    CreateMap<UpdatePatientCommand, Patient>();
    
    CreateMap<CreateUserCommand, AppUser>();
    CreateMap<UpdateUserCommand, AppUser>();
  }
}