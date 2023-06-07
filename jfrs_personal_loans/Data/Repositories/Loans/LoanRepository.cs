using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.Repositories.Loans
{
    public class LoanRepository : SQLRepository<Loan>, ILoanRepository
    {
        public LoanRepository(JFRSPersonalLoansDBContext context) : base(context)
        {

        }
    }
}
