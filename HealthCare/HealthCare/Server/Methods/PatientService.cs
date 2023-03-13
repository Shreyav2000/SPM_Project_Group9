using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.EntityFrameworkCore;

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
                          select new AttendanceObject
                          {
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
    
    }
}
