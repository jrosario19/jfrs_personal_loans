using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.CompanyConfigurations
{
    public interface ICompanyConfigurationService
    {
        CompanyConfiguration InsertCompanyConfiguration(CompanyConfiguration CompanyConfiguration);
        IQueryable<CompanyConfiguration> GetCompanyConfigurations();

        CompanyConfiguration GetCompanyConfigurationById(int? Id);
        CompanyConfiguration UpdateCompanyConfiguration(CompanyConfiguration CompanyConfiguration);
        CompanyConfiguration DeleteCompanyConfiguration(CompanyConfiguration CompanyConfiguration);
    }
}
