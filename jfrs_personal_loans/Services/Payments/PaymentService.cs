using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_payments.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Payment InsertPayment(Payment Payment)
        {
            Payment payment = _unitOfWork.PaymentRepository.Insert(Payment);
            _unitOfWork.SaveChanges();
            return payment;
        }
        public Payment GetPaymentById(int? Id)
        {
            return _unitOfWork.PaymentRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<Payment> GetPayments()
        {
            return _unitOfWork.PaymentRepository.GetAll();
        }
        public Payment UpdatePayment(Payment Payment)
        {
            Payment payment = _unitOfWork.PaymentRepository.Update(Payment);
            _unitOfWork.SaveChanges();
            return payment;
        }
        public Payment DeletePayment(Payment Payment)
        {
            //Payment.IsActive = false;
            Payment payment = _unitOfWork.PaymentRepository.Delete(Payment);
            _unitOfWork.SaveChanges();
            return payment;
            //return _PaymentRepository.Delete(Payment);
        }
    }
}
