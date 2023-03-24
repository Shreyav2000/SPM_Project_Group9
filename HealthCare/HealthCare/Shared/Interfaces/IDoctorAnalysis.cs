using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface IDoctorAnalysis
    {
        Task<List<TopPrescribedDrug>> TopPrescribedDrugs(DateTime a_start,DateTime a_end,int a_doctorId);
        Task<List<Cases>> FrequentCases(DateTime a_start, DateTime a_end, int a_doctorId);
        Task<List<Cases>> Cases(DateTime a_start, DateTime a_end, int a_doctorId);
    }
}
