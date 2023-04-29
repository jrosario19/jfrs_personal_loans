using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class CompanyConfiguration:BaseEntity, ITenantEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Currency { get; set; }
        public string TenantId { get; set; }
    }
}
