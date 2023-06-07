using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Installments
{
    public class InstallmentService : IInstallmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Installment InsertInstallment(Installment Installment)
        {
            Installment installment = _unitOfWork.InstallmentRepository.Insert(Installment);
            _unitOfWork.SaveChanges();
            return installment;
        }
        public Installment GetInstallmentById(int? Id)
        {
            return _unitOfWork.InstallmentRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<Installment> GetInstallments()
        {
            return _unitOfWork.InstallmentRepository.GetAll();
        }
        public Installment UpdateInstallment(Installment Installment)
        {
            Installment installment = _unitOfWork.InstallmentRepository.Update(Installment);
            _unitOfWork.SaveChanges();
            return installment;
        }
        public Installment DeleteInstallment(Installment Installment)
        {
            Installment.IsActive = false;
            Installment installment = _unitOfWork.InstallmentRepository.Update(Installment);
            _unitOfWork.SaveChanges();
            return installment;
            //return _InstallmentRepository.Delete(Installment);
        }
    }
}
