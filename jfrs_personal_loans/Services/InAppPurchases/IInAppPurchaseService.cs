using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.InAppPurchases
{
    public interface IInAppPurchaseService
    {
        InAppPurchase InsertInAppPurchase(InAppPurchase InAppPurchase);
        IQueryable<InAppPurchase> GetInAppPurchases();

        InAppPurchase GetInAppPurchaseById(int? Id);
        InAppPurchase UpdateInAppPurchase(InAppPurchase InAppPurchase);
        InAppPurchase DeleteInAppPurchase(InAppPurchase InAppPurchase);
    }
}
