using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.CompanyConfigurations
{
    public class CompanyConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CompanyConfiguration InsertCompanyConfiguration(CompanyConfiguration CompanyConfiguration)
        {
            CompanyConfiguration clientDeliveryPoint = _unitOfWork.CompanyConfigurationRepository.Insert(CompanyConfiguration);
            _unitOfWork.SaveChanges();
            return clientDeliveryPoint;
        }
        public CompanyConfiguration GetCompanyConfigurationById(int? Id)
        {
            return _unitOfWork.CompanyConfigurationRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<CompanyConfiguration> GetCompanyConfigurations()
        {
            return _unitOfWork.CompanyConfigurationRepository.GetAll();
        }
        public CompanyConfiguration UpdateCompanyConfiguration(CompanyConfiguration CompanyConfiguration)
        {
            CompanyConfiguration clientDeliveryPoint = _unitOfWork.CompanyConfigurationRepository.Update(CompanyConfiguration);
            _unitOfWork.SaveChanges();
            return clientDeliveryPoint;
        }
        public CompanyConfiguration DeleteCompanyConfiguration(CompanyConfiguration CompanyConfiguration)
        {
            CompanyConfiguration.IsActive = false;
            CompanyConfiguration clientDeliveryPoint = _unitOfWork.CompanyConfigurationRepository.Update(CompanyConfiguration);
            _unitOfWork.SaveChanges();
            return clientDeliveryPoint;
            //return _CompanyConfigurationRepository.Delete(CompanyConfiguration);
        }
    }
}
