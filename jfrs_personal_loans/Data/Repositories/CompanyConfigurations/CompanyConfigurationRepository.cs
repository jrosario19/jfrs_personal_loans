using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.CompanyConfigurations
{
    public class CompanyConfigurationRepository:SQLRepository<CompanyConfiguration>,ICompanyConfigurationRepository
    {
        public CompanyConfigurationRepository(JFRSPersonalLoansDBContext context):base(context)
        {

        }
    }
}
