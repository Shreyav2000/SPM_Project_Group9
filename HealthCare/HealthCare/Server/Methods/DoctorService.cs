using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server.Methods
{
    public class DoctorService : IDoctorService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<DrugService> m_logger;

        public DoctorService(HealthcareContext context, ILogger<DrugService> logger)
        {
            m_context = context;
            m_logger = logger;
        }
        /// <summary>
        /// Returns a list of cases a doctor dealt within a specified period
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <param name="a_doctorId"></param>
        /// <returns>List of complaints</returns>
        public async Task<List<Cases>> Cases(DateTime a_start, DateTime a_end, int a_doctorId)
        {
            return await (from pa in m_context.Patientattendances
                          join n in m_context.Patientcomplaints on pa.ConsultId equals n.ConsultId
                          join t in m_context.Complaints on n.ComplaintId equals t.ComplaintId
                          where pa.SeenByDoctorId == a_doctorId && pa.PTime.Date >= a_start && pa.PTime.Date <= a_end
                          group t by new { pa.PTime.Date } into g
                          orderby g.Key.Date.Date
                          select new Cases
                          {
                              Count = g.Count(),
                              Day = g.Key.Date.ToString("dd, MMMM"),
                          }).Distinct().ToListAsync();
        }

        /// <summary>
        /// Returns a list of top 5 frequent cases a doctor dealt within a specified period
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <param name="a_doctorId"></param>
        /// <returns>List of complaints</returns>
        public async Task<List<Cases>> FrequentCases(DateTime a_start, DateTime a_end, int a_doctorId)
        {
            return await (from pa in m_context.Patientattendances
                          join n in m_context.Patientcomplaints on pa.ConsultId equals n.ConsultId
                          join t in m_context.Complaints on n.ComplaintId equals t.ComplaintId
                          where pa.SeenByDoctorId == a_doctorId && pa.PTime.Date >= a_start && pa.PTime.Date <= a_end
                          group t by new { t.Complaint1 } into g
                          select new Cases
                          {
                              Count = g.Count(),
                              Case = g.Key.Complaint1,
                          }).Take(5).ToListAsync();
        }
        /// <summary>
        /// Saves a consultation session to the database
        /// </summary>
        /// <param name="sessionObject"></param>
        /// <returns></returns>
        public async Task<bool> SubmitSession(SessionObject a_sessionObject, int a_doctorId)
        {
            Patientattendance attendance = new Patientattendance
            {
                ConsultId = a_sessionObject.Id,
                PatientId = a_sessionObject.Patient.PatientId,
                PatientNo = a_sessionObject.Patient.PatientNo!,
                SeenByDoctorId = a_doctorId,
                PTime = DateTime.Now,
                SeenByDoctorTime = DateTime.Now,
                SeenDoctor = true,
                PatListdate = DateOnly.FromDateTime(DateTime.Now),
                FinalDischargeDate = DateTime.Now,
            };

            Patientcomplaintnote notes = new Patientcomplaintnote
            {
                ConsultId = a_sessionObject.Id,
                Notes = a_sessionObject.ComplaintNotes,
                PatientComplaintNotesDate = DateTime.Now,
                PatientId = a_sessionObject.Patient.PatientId,
                UserId = a_doctorId,
            };

            Prescription prescription = new Prescription
            {
                Id = int.Parse(String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000)),
                PatientId = a_sessionObject.Patient.PatientId,
                ConsId = a_sessionObject.Id,
                PatientNo = a_sessionObject.Patient.PatientNo!,
                DoctorRequesting = a_doctorId,
                TransDate = DateTime.Now,
                PrescriptionSessionId = $"DR{a_sessionObject.Id}",
            };
            try
            {
                m_context.Patientattendances.Add(attendance);
                m_context.Patientcomplaintnotes.Add(notes);
                m_context.Prescriptions.Add(prescription);
                m_context.Prescriptiondetails.AddRange(a_sessionObject.Prescriptions);
                m_context.Patientcomplaints.AddRange(a_sessionObject.Complaints);

                return await m_context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the list of top 5 drugs prescribed by specific doctor within a specific period
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <param name="a_doctorId"></param>
        /// <returns></returns>
        public async Task<List<TopPrescribedDrug>> TopPrescribedDrugs(DateTime a_start, DateTime a_end, int a_doctorId)
        {
            return await (from pa in m_context.Patientattendances
                          join ps in m_context.Prescriptiondetails on pa.ConsultId equals ps.ConsId
                          join d in m_context.Drugs on ps.DrugId equals d.DrugId
                          where pa.SeenByDoctorId == a_doctorId && pa.PTime.Date >= a_start && pa.PTime.Date <= a_end
                          group d by new { d.DrugId, d.Drugname } into g
                          select new TopPrescribedDrug
                          {
                              Drug = g.Key.Drugname,
                              Count = g.Count(),
                          }).Take(5).ToListAsync();
        }
    }
}
