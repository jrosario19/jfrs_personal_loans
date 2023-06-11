using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.Repositories.Payments
{
    public class PaymentRepository:SQLRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(JFRSPersonalLoansDBContext context) : base(context)
        {

        }
    }
}
