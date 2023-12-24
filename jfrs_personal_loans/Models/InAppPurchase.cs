using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class InAppPurchase : BaseEntity, ITenantEntity
    {
        public string InAppPurchaseToken { get; set; }
        public string ProductId { get; set; }
        public string TenantId { get; set; }
    }
}
