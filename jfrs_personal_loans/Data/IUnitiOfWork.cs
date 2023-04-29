using jfrs_personal_loans.Data.CompanyConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool SaveChanges();
        CompanyConfigurationRepository CompanyConfigurationRepository { get; }
    }
}
