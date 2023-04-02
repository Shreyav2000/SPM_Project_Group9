using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server.Methods
{
    public class ComplaintService : IComplaintService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<ComplaintService> m_logger;

        public ComplaintService(HealthcareContext context, ILogger<ComplaintService> logger)
        {
            m_context = context;
            m_logger = logger;
        }
        public async Task<List<Complaint>> GetComplaint()
        {
            return await m_context.Complaints.ToListAsync();
        }
    }
}
