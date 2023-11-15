﻿using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.Repositories.InAppPurchases
{
    public class InAppPurchaseRepository : SQLRepository<InAppPurchase>, IInAppPurchaseRepository
    {
        public InAppPurchaseRepository(JFRSPersonalLoansDBContext context) : base(context)
        {

        }
    }
}
