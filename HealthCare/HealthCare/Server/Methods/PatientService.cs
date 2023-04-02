using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace HealthCare.Server.Methods
{
    public class PatientService : IPatientService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<PatientService> m_logger;

        public PatientService(HealthcareContext context, ILogger<PatientService> logger)
        {
            m_context = context;
            m_logger = logger;
        }

        /// <summary>
        /// Get user names and doctor names who they have consulted
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <returns>list of users with doctors from consultations</returns>
        public async Task<List<UserConsults>> GetUserConsultations()
        {
            //return await(from pa in m_context.Patientattendances
            //             join p in m_context.Patients on pa.PatientId equals p.PatientId
            //             join s in m_context.Staff on pa.SeenByDoctorId equals s.Staffid
            //             select new UserConsults
            //             {
            //                 PatientId = p.PatientId,
            //                 PatientName = p.Fname + " " + p.Lname,
            //                 DoctorName = s.Fname + " " + s.Lname
            //             }).ToListAsync();
           return await m_context.Patientattendances
                    .Join(m_context.Patients,
                        pa => pa.PatientId,
                        p => p.PatientId,
                        (pa, p) => new { pa, p })
                    .Join(m_context.Staff,
                        pa_p => pa_p.pa.SeenByDoctorId,
                        s => s.Staffid,
                        (pa_p, s) => new UserConsults
                        {
                            PatientId = pa_p.p.PatientId,
                            PatientName = pa_p.p.Fname + " " + pa_p.p.Lname,
                            DoctorName = s.Fname + " " + s.Lname
                        })
                    .ToListAsync();
        }

        /// <summary>
        /// Gets attendance for patient during a specified period
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <returns>List of Attendance records</returns>
        public async Task<List<AttendanceObject>> GetAttendance(DateTime a_start, DateTime a_end)
        {
            return await (from pa in m_context.Patientattendances
                          join p in m_context.Patients on pa.PatientId equals p.PatientId
                          join s in m_context.Staff on pa.SeenByDoctorId equals s.Staffid
                          join n in m_context.Patientcomplaintnotes on pa.ConsultId equals n.ConsultId into b
                          where pa.PTime.Date >= a_start && pa.PTime.Date <= a_end
                          select new AttendanceObject
                          {
                              Notes = b.FirstOrDefault(t => !string.IsNullOrEmpty(t.Notes)).Notes,
                              ConsultId = pa.ConsultId,
                              DoctorId = pa.SeenByDoctorId,
                              PatientId = pa.PatientNo,
                              Timestamp = pa.PTime,
                              PatientName = p.Fname + " " + p.Lname,
                              DoctorName = s.Fname + " " + s.Lname
                          }).ToListAsync();


        }
        /// <summary>
        /// Gets attendance for patient during a specified period seen by specific doctor
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <param name="a_doctorId"></param>
        /// <returns>List of Attendance records</returns>
        public async Task<List<AttendanceObject>> GetAttendance(DateTime a_start, DateTime a_end, int? a_doctorId)
        {
            return await (from pa in m_context.Patientattendances
                          join p in m_context.Patients on pa.PatientId equals p.PatientId
                          join s in m_context.Staff on pa.SeenByDoctorId equals s.Staffid
                          join n in m_context.Patientcomplaintnotes on pa.ConsultId equals n.ConsultId into b
                          where s.Staffid == a_doctorId && pa.PTime.Date >= a_start && pa.PTime.Date <= a_end
                          select new AttendanceObject
                          {
                              Notes = b.FirstOrDefault().Notes,
                              ConsultId = pa.ConsultId,
                              DoctorId = pa.SeenByDoctorId,
                              PatientId = pa.PatientNo,
                              Timestamp = pa.PTime,
                              PatientName = p.Fname + " " + p.Lname,
                              DoctorName = s.Fname + " " + s.Lname
                          }).ToListAsync();
        }

        /// <summary>
        /// Gets the Profile of a patient using the patients's id provided
        /// </summary>
        /// <param name="a_name"></param>
        /// <returns>Patient Profile if found</returns>
        public Patient GetPatientProfile(string a_name)
        {
            return m_context.Patients.FirstOrDefault(n => n.PatientNo == a_name);
        }

        /// <summary>
        /// Gets the medical history of the patient id provided
        /// </summary>
        /// <param name="a_patientId"></param>
        /// <returns>All previous medical records</returns>
        public async Task<List<MedHistory>> GetPatientRecord(int a_patientId)
        {
            return await (
                from pa in m_context.Patientattendances
                join s in m_context.Staff on pa.SeenByDoctorId equals s.Staffid
                join c in m_context.Patientcomplaintnotes on pa.ConsultId equals c.ConsultId
                join ps in m_context.Prescriptiondetails on pa.ConsultId equals ps.ConsId into prescriptions
                where pa.PatientId == pa.PatientId
                select new MedHistory
                {
                    ConsultId = pa.ConsultId,
                    Complaint = c.Notes,
                    Timestamp = pa.PTime,
                    DoctorName = s.Fname + " " + s.Lname
                }).ToListAsync();
             }
        /// <summary>
        /// Gets a list of patients by the specified id
        /// </summary>
        /// <param name="a_patientId"></param>
        /// <returns></returns>
        public Task<List<Patient>> GetPatients(string a_patientId)
        {
            if (string.IsNullOrEmpty(a_patientId))
            {
                return m_context.Patients.Take(100).ToListAsync();
            }
            return m_context.Patients.Where(i => i.PatientNo.StartsWith(a_patientId) || i.PatientId.ToString().StartsWith(a_patientId)).Take(100).ToListAsync();
        }
    }
    }
