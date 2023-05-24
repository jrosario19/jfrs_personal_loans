using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.LoanConfigurations
{
    public class LoanConfigurationService: ILoanConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public LoanConfiguration InsertLoanConfiguration(LoanConfiguration LoanConfiguration)
        {
            LoanConfiguration loanConfiguration = _unitOfWork.LoanConfigurationRepository.Insert(LoanConfiguration);
            _unitOfWork.SaveChanges();
            return loanConfiguration;
        }
        public LoanConfiguration GetLoanConfigurationById(int? Id)
        {
            return _unitOfWork.LoanConfigurationRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<LoanConfiguration> GetLoanConfigurations()
        {
            return _unitOfWork.LoanConfigurationRepository.GetAll();
        }
        public LoanConfiguration UpdateLoanConfiguration(LoanConfiguration LoanConfiguration)
        {
            LoanConfiguration loanConfiguration = _unitOfWork.LoanConfigurationRepository.Update(LoanConfiguration);
            _unitOfWork.SaveChanges();
            return loanConfiguration;
        }
        public LoanConfiguration DeleteLoanConfiguration(LoanConfiguration LoanConfiguration)
        {
            LoanConfiguration.IsActive = false;
            LoanConfiguration loanConfiguration = _unitOfWork.LoanConfigurationRepository.Update(LoanConfiguration);
            _unitOfWork.SaveChanges();
            return loanConfiguration;
            //return _LoanConfigurationRepository.Delete(LoanConfiguration);
        }
    }
}
