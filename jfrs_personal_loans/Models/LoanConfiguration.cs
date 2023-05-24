using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class LoanConfiguration : BaseEntity, ITenantEntity
    {
        public string PaymentFrequency { get; set; }
        public string InterestApplication { get; set; }
        public int DefaultInterest { get; set; }
        public bool IsAllowLoanArrears { get; set; }
        public string LoanArrearsApplication { get; set; }
        public int LoanArrearsInterest { get; set; }
        public int LoanArrearsAllowDays { get; set; }
        public string IgnoredWeekDays { get; set; }
        public int MonthDays { get; set; }
        public int FortnightDays { get; set; }
        public bool IsAllowToSetDayForPayment { get; set; }
        public int PaymentWeekDay { get; set; }
        public int PaymentFirstFortnightDay { get; set; }
        public int PaymentSecondFortnightDay { get; set; }
        public int PaymentMonthtDay { get; set; }
        public string TenantId { get; set; }
    }
}
