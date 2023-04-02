using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server.Methods
{
    public class DrugService : IDrugService
    {
        private readonly HealthcareContext m_context;
        private readonly ILogger<DrugService> m_logger;

        public DrugService(HealthcareContext context, ILogger<DrugService> logger)
        {
            m_context = context;
            m_logger = logger;
        }
        /// <summary>
        /// Creates a new drug
        /// </summary>
        /// <param name="a_drug"></param>
        /// <returns>True if drug created successfully</returns>
        public bool CreateDrug(DrugModel a_drug)
        {
            try
            {
                string id = $"DR{String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000)}";
                a_drug.Drug.SetId(id);
                a_drug.Stock.SetId(id);
                m_context.Drugs.Add(a_drug.Drug);
                m_context.DrugStocks.Add(a_drug.Stock);
                if (m_context.SaveChanges() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Decommissions a drug 
        /// </summary>
        /// <param name="a_drugId"></param>
        /// <returns>True if drug decomissioned successfully</returns>
        public bool DecomissionDrug(string a_drugId)
        {
            m_context.Drugs.First(i => i.DrugId == a_drugId).OutOfService = true;
            if (m_context.SaveChanges() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Reinstates a decomissioned drug 
        /// </summary>
        /// <param name="a_drugId"></param>
        /// <returns>True if drug reinstated successfully</returns>
        public bool ReinstateDrug(string a_drugId)
        {
            m_context.Drugs.First(i => i.DrugId == a_drugId).OutOfService = false;
            if (m_context.SaveChanges() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Returns list of drugs specified by name
        /// </summary>
        /// <param name="a_name"></param>
        /// <returns>List of durgs</returns>
        public List<Drug> GetDrugByName(string a_name)
        {
            return m_context.Drugs.Where(i => i.Drugname!.Contains(a_name)).ToList();
        }
        /// <summary>
        /// Returns a DrugItem by the specified Id
        /// </summary>
        /// <param name="a_drugId"></param>
        /// <returns></returns>
        public Drug? GetDrugById(string a_drugId)
        {
            return m_context.Drugs.FirstOrDefault(i => i.DrugId == a_drugId);
        }

        /// <summary>
        /// Updates a drug
        /// </summary>
        /// <param name="a_drug"></param>
        /// <returns>True if drug updated successfully</returns>
        public bool UpdateDrug(DrugModel a_drug)
        {
            Drug drug = m_context.Drugs.First(d => d.DrugId == a_drug.Drug.DrugId);
            drug = a_drug.Drug;

            if (m_context.SaveChanges() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Restocks drug quantity
        /// </summary>
        /// <param name="a_item"></param>
        /// <returns>True if quantity updated successfully</returns>
        public bool RestockDrug(StockItem a_item)
        {
            if (a_item.Quantity > 0)
                m_context.Drugs.First(i => i.DrugId == a_item.DrugId).Refill = false;

            m_context.DrugStocks.First(i => i.DrugId == a_item.DrugId).Quantity += a_item.Quantity;
            if (m_context.SaveChanges() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Marks a drug as needing refill or restock
        /// </summary>
        /// <param name="a_drugId"></param>
        /// <returns>True if successful</returns>
        public bool MarkForRefill(string a_drugId)
        {
            m_context.Drugs.First(i => i.DrugId == a_drugId).Refill = true;
            if (m_context.SaveChanges() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Marks drugs as expired
        /// </summary>
        /// <param name="a_drugId"></param>
        /// <param name="a_quantity"></param>
        /// <returns>True if quantity updated successfully</returns>
        public bool MarkAsExpired(Expiryitem a_drug)
        {
            m_context.DrugStocks.First(i => i.DrugId == a_drug.ItemId).Quantity -= a_drug.Quantity;
            m_context.Expiryitems.Add(a_drug);
            if (m_context.SaveChanges() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// List of all drugs in the system
        /// </summary>
        public List<Drug> GetDrugs()
        {
            return m_context.Drugs.Where(t => !t.OutOfService).ToList();
        }
        /// <summary>
        /// Gets analysis of drugs usuages in the system
        /// </summary>
        /// <param name="a_start"></param>
        /// <param name="a_end"></param>
        /// <returns>Returns analysis</returns>
        public DrugAnalysis GetAnalysis(DateTime a_start, DateTime a_end)
        {
            DrugAnalysis analysis = new DrugAnalysis();
            var bills = m_context.Bills.AsNoTracking().Where(t => t.BillDate >= a_start && t.BillDate <= a_end).Select(i => i.BillId).ToList();
            string? topDrugId = m_context.Billdetails.Where(t => bills.Contains(t.BillId))
            .GroupBy(t => t.ItemId)
            .Select(g => new { ItemId = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .FirstOrDefault()?.ItemId;

            if (topDrugId == null)
                topDrugId = string.Empty;
            var prescriptions = m_context.Prescriptiondetails.AsNoTracking().Where(t => t.Ddate >= a_start && t.Ddate <= a_end).Select(i => i.PrescriptionSessionId).ToList();
            string mostPrescribedId = string.Empty;
            if (prescriptions.Count > 0)
                mostPrescribedId = m_context.Prescriptiondetails.Where(t => prescriptions.Contains(t.PrescriptionSessionId))
                .GroupBy(t => t.DrugId)
                .Select(g => new { ItemId = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault().ItemId;
            var outDrugs = m_context.DrugStocks.Where(i => i.Quantity <= 0).Select(i => i.DrugId).ToList();
            analysis.ExpiredDrugs = m_context.Expiryitems.Where(i => i.ExpDate > a_start && i.ExpDate < a_end).ToList();
            analysis.ReplenishedDrugs = m_context.Drugs.AsNoTracking().Where(i => outDrugs.Contains(i.DrugId)).ToList();
            analysis.MostPurchasedDrug = m_context.Drugs.FirstOrDefault(t => t.DrugId == topDrugId)?.Drugname;
            analysis.MostExpensiveDrug = m_context.Drugs.OrderByDescending(d => d.Price).First().Drugname;
            analysis.MostPrescribedDrug = m_context.Drugs.FirstOrDefault(t => t.DrugId == mostPrescribedId)?.Drugname;
            return analysis;
        }
    }
}
