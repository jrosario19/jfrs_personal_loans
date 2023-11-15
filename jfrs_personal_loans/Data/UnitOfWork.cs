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
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly JFRSPersonalLoansDBContext _context;
        public UnitOfWork(JFRSPersonalLoansDBContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    returnValue = false;
                    dbContextTransaction.Rollback();

                }
                return returnValue;
            }
        }

        private CompanyConfigurationRepository _companyConfigurationRepository;
        public CompanyConfigurationRepository CompanyConfigurationRepository => _companyConfigurationRepository ?? (_companyConfigurationRepository = new CompanyConfigurationRepository(_context));

        private LoanConfigurationRepository _loanConfigurationRepository;
        public LoanConfigurationRepository LoanConfigurationRepository => _loanConfigurationRepository ?? (_loanConfigurationRepository = new LoanConfigurationRepository(_context));

        private ClientRepository _clientRepository;
        public ClientRepository ClientRepository => _clientRepository ?? (_clientRepository = new ClientRepository(_context));

        private LoanRepository _loanRepository;
        public LoanRepository LoanRepository => _loanRepository ?? (_loanRepository = new LoanRepository(_context));

        private InstallmentRepository _installmentRepository;
        public InstallmentRepository InstallmentRepository => _installmentRepository ?? (_installmentRepository = new InstallmentRepository(_context));

        private PaymentRepository _paymentRepository;
        public PaymentRepository PaymentRepository => _paymentRepository ?? (_paymentRepository = new PaymentRepository(_context));

        private InAppPurchaseRepository _inAppPurchaseRepository;
        public InAppPurchaseRepository InAppPurchaseRepository => _inAppPurchaseRepository ?? (_inAppPurchaseRepository = new InAppPurchaseRepository(_context));

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                //dispose managed state (managed objects).
            }
            _disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
