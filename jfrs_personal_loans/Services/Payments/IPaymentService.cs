using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Payments
{
    public interface IPaymentService
    {
        Payment InsertPayment(Payment Payment);
        IQueryable<Payment> GetPayments();

        Payment GetPaymentById(int? Id);
        Payment UpdatePayment(Payment Payment);
        Payment DeletePayment(Payment Payment);
    }
}
