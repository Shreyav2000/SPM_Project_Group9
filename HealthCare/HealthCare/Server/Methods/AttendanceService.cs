using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Objects;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server.Methods
{
    public class AttendanceService : IPatientService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<AttendanceService> m_logger;

        public AttendanceService(HealthcareContext context, ILogger<AttendanceService> logger)
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
    }
}
