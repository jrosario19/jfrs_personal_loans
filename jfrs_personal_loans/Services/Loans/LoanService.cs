using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Loans
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Loan InsertLoan(Loan Loan)
        {
            Loan loan = _unitOfWork.LoanRepository.Insert(Loan);
            _unitOfWork.SaveChanges();
            return loan;
        }
        public Loan GetLoanById(int? Id)
        {
            return _unitOfWork.LoanRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<Loan> GetLoans()
        {
            return _unitOfWork.LoanRepository.GetAll();
        }
        public Loan UpdateLoan(Loan Loan)
        {
            Loan loan = _unitOfWork.LoanRepository.Update(Loan);
            _unitOfWork.SaveChanges();
            return loan;
        }
        public Loan DeleteLoan(Loan Loan)
        {
            //Loan.IsActive = false;
            Loan loan = _unitOfWork.LoanRepository.Delete(Loan);
            _unitOfWork.SaveChanges();
            return loan;
            //return _LoanRepository.Delete(Loan);
        }
    }
}
