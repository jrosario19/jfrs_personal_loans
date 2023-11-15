using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.InAppPurchases
{
    public class InAppPurchaseService : IInAppPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InAppPurchaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public InAppPurchase InsertInAppPurchase(InAppPurchase InAppPurchase)
        {
            InAppPurchase inAppPurchase = _unitOfWork.InAppPurchaseRepository.Insert(InAppPurchase);
            _unitOfWork.SaveChanges();
            return inAppPurchase;
        }
        public InAppPurchase GetInAppPurchaseById(int? Id)
        {
            return _unitOfWork.InAppPurchaseRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<InAppPurchase> GetInAppPurchases()
        {
            return _unitOfWork.InAppPurchaseRepository.GetAll();
        }
        public InAppPurchase UpdateInAppPurchase(InAppPurchase InAppPurchase)
        {
            InAppPurchase inAppPurchase = _unitOfWork.InAppPurchaseRepository.Update(InAppPurchase);
            _unitOfWork.SaveChanges();
            return inAppPurchase;
        }
        public InAppPurchase DeleteInAppPurchase(InAppPurchase InAppPurchase)
        {
            InAppPurchase.IsActive = false;
            InAppPurchase inAppPurchase = _unitOfWork.InAppPurchaseRepository.Update(InAppPurchase);
            _unitOfWork.SaveChanges();
            return inAppPurchase;
            //return _InAppPurchaseRepository.Delete(InAppPurchase);
        }
    }
}
