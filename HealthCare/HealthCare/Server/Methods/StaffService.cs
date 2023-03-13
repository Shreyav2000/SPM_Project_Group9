using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;

namespace HealthCare.Server.Methods
{
    public class StaffService : IStaffService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<Staff> m_logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Staff"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger.</param>
        public StaffService(HealthcareContext context, ILogger<Staff> logger)
        {
            m_context = context;
            m_logger = logger;
        }
        /// <summary>
        /// Looks up a staff by their staff id
        /// </summary>
        /// <param name="a_staffid"></param>
        /// <returns>Staff profile is found</returns>
        public Staff GetStaff(int a_staffid)
        {
            return m_context.Staff.FirstOrDefault(i => i.Staffid == a_staffid);
        }
    }
}
