﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public interface ITenantEntity
    {
        string TenantId { get; set; }
    }
}
