using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.LoanConfigurations
{
    public interface ILoanConfigurationService
    {
        LoanConfiguration InsertLoanConfiguration(LoanConfiguration LoanConfiguration);
        IQueryable<LoanConfiguration> GetLoanConfigurations();

        LoanConfiguration GetLoanConfigurationById(int? Id);
        LoanConfiguration UpdateLoanConfiguration(LoanConfiguration LoanConfiguration);
        LoanConfiguration DeleteLoanConfiguration(LoanConfiguration LoanConfiguration);
    }
}
