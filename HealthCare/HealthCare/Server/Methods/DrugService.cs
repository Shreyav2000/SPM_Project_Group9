using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Models;

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
                a_drug.Drugitem.SetItemId(id);
                m_context.Drugs.Add(a_drug.Drug);
                m_context.Drugitems.Add(a_drug.Drugitem);

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
            m_context.Drugitems.First(i => i.ItemId == a_drugId).OutOfService = true;
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
            m_context.Drugitems.First(i => i.ItemId == a_drugId).OutOfService = false;
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
        public Drugitem? GetDrugItemById(string a_drugId)
        {
            return m_context.Drugitems.FirstOrDefault(i => i.ItemId == a_drugId);
        }

        /// <summary>
        /// Updates a drug
        /// </summary>
        /// <param name="a_drug"></param>
        /// <returns>True if drug updated successfully</returns>
        public bool UpdateDrug(DrugModel a_drug)
        {
            Drugitem item = m_context.Drugitems.First(d => d.ItemId == a_drug.Drugitem.ItemId);
            Drug drug = m_context.Drugs.First(d => d.DrugId == a_drug.Drug.DrugId);

            drug = a_drug.Drug;
            item = a_drug.Drugitem;

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
                m_context.Drugitems.First(i => i.ItemId == a_item.DrugId).Refill = false;

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
            m_context.Drugitems.First(i => i.ItemId == a_drugId).Refill = true;
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
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Drug> GetDrugs()
        {
            return m_context.Drugs.ToList();
        }
    }
}
