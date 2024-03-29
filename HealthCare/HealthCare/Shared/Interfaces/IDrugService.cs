﻿using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface IDrugService
    {
        bool CreateDrug(DrugModel a_drug);
        bool UpdateDrug(DrugModel a_drug);
        bool DecomissionDrug(string a_drugId);
        bool ReinstateDrug(string a_drugId);
        bool RestockDrug(StockItem a_item);
        bool MarkForRefill(string a_drugId);
        bool MarkAsExpired(Expiryitem a_drug);
        DrugAnalysis GetAnalysis(DateTime a_start, DateTime a_end);
        Drug? GetDrugById(string a_drugId);
        List<Drug> GetDrugByName(string a_name);
        List<Drug> GetDrugs();
    }
}
