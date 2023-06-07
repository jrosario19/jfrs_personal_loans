using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Loans
{
    public interface ILoanService
    {
        Loan InsertLoan(Loan Loan);
        IQueryable<Loan> GetLoans();

        Loan GetLoanById(int? Id);
        Loan UpdateLoan(Loan Loan);
        Loan DeleteLoan(Loan Loan);
    }
}
