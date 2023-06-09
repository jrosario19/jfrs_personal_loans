﻿using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class Payment : BaseEntity, ITenantEntity
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public decimal ArrearAmount { get; set; }
        public string Status { get; set; }
        public string TenantId { get; set; }
        public int LoanId { get; set; }
    }
}
