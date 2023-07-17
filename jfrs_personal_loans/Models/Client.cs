using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class Client : BaseEntity, ITenantEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Code { get; set; }
        public string CellPhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ReferalName { get; set; }
        public string ReferalPhonenumber { get; set; }
        public string ReferalAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TenantId { get; set; }
    }
}
