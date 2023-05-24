using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.Repositories.LanConfigurations
{
    public class LoanConfigurationRepository : SQLRepository<LoanConfiguration>, ILoanConfigurationRepository
    {
        public LoanConfigurationRepository(JFRSPersonalLoansDBContext context) : base(context)
        {

        }
    }
}
