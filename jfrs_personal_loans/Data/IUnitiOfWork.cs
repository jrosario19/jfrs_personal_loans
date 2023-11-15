using jfrs_personal_loans.Data.CompanyConfigurations;
using jfrs_personal_loans.Data.Repositories.Clients;
using jfrs_personal_loans.Data.Repositories.InAppPurchases;
using jfrs_personal_loans.Data.Repositories.Installments;
using jfrs_personal_loans.Data.Repositories.LanConfigurations;
using jfrs_personal_loans.Data.Repositories.Loans;
using jfrs_personal_loans.Data.Repositories.Payments;
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
        LoanConfigurationRepository LoanConfigurationRepository { get; }
        ClientRepository ClientRepository { get; }
        LoanRepository LoanRepository { get; }
        InstallmentRepository InstallmentRepository { get; }
        PaymentRepository PaymentRepository { get; }
        InAppPurchaseRepository InAppPurchaseRepository { get; }

    }
}
