using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server.Methods
{
    public class DoctorService : IDoctorAnalysis
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
            return await(from pa in m_context.Patientattendances
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
                          group t by new { t.Complaint1} into g
                          select new Cases
                          {
                             Count = g.Count(),
                             Case = g.Key.Complaint1,
                         }).Take(5).ToListAsync();
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
                          group d by new { d.DrugId,d.Drugname } into g                          
                         select new TopPrescribedDrug
                         {
                             Drug = g.Key.Drugname,
                             Count = g.Count(),
                         }).Take(5).ToListAsync();
        }
    }
}
