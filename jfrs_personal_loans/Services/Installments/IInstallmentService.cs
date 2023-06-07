using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Installments
{
    public interface IInstallmentService
    {
        Installment InsertInstallment(Installment Installment);
        IQueryable<Installment> GetInstallments();

        Installment GetInstallmentById(int? Id);
        Installment UpdateInstallment(Installment Installment);
        Installment DeleteInstallment(Installment Installment);
    }
}
