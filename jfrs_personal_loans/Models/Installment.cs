﻿using jfrs_personal_loans.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class Installment : BaseEntity, ITenantEntity
    {
        [Column(TypeName = "date")]
        public DateTime OpenDate { get; set; }
        public int InstallmentAmount { get; set; }
        public string Status { get; set; }
        public string TenantId { get; set; }
        [Required]
        public int LoanId { get; set; }
        public virtual Loan Loan { get; set; }
    }
}
