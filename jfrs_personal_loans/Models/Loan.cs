using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class Loan : BaseEntity, ITenantEntity
    {
        public string OpenDate { get; set; }
        public string DueDate { get; set; }
        public int Capital { get; set; }
        public decimal Rate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentFrequency { get; set; }
        public int installmentQty { get; set; }
        public decimal InstallmentAmount { get; set; }
        public string Status { get; set; }
        public bool IsAllowLoanArrears { get; set; }
        public decimal LoanArrearsInterest { get; set; }
        public int LoanArrearsAllowDays { get; set; }
        [Required]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string TenantId { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
