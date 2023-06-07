using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.Repositories.Installments
{
    public class InstallmentRepository : SQLRepository<Installment>, IInstallmentrepository
    {
        public InstallmentRepository(JFRSPersonalLoansDBContext context) : base(context)
        {

        }
    }
}
