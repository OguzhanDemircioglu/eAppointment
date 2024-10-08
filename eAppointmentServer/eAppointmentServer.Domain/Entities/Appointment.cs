﻿using eAppointmentServer.Domain.Abstractions;

namespace eAppointmentServer.Domain.Entities;

public sealed class Appointment : Entity
{
    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsComplated { get; set; }
}