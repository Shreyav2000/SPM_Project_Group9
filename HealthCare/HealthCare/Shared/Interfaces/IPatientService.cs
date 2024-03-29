﻿using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface IPatientService
    {
        Task<List<AttendanceObject>> GetAttendance(DateTime a_start,DateTime a_end);
        Task<List<AttendanceObject>> GetAttendance(DateTime a_start, DateTime a_end,int? a_doctorId);
        Task<List<MedHistory>> GetPatientRecord(int a_patientId);
        Patient GetPatientProfile(string a_patientId);
        Task<List<Patient>> GetPatients(string a_patientId);
        Task<List<UserConsults>> GetUserConsultations();
    }
}
