using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.Loans;
using jfrs_personal_loans.Services.Payments;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jfrs_personal_loans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoanService _loanService;
        private readonly ITenantService _tenantService;

        public PaymentsController(IPaymentService paymentService, ILoanService loanService, ITenantService tenantService)
        {
            this._paymentService = paymentService;
            this._loanService = loanService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var payments = _paymentService.GetPayments().ToList();
            List<Payment> newPayments = new List<Payment>();


            foreach (var payment1 in payments)
            {
                var payment = _paymentService.GetPaymentById(payment1.Id);
                var loanList = _loanService.GetLoans().ToList();
                var loanFEId = loanList.FirstOrDefault(l => l.Id == payment.LoanId).FEId;
                payment.LoanId = int.Parse(loanFEId);
                newPayments.Add(payment);
            }


            return Ok(new { payments = newPayments });
        }

        [HttpGet("{id}", Name = "CreatedPayment")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            var loanList = _loanService.GetLoans().ToList();
            var loanFEId = loanList.FirstOrDefault(l => l.Id == payment.LoanId).FEId;
            payment.LoanId = int.Parse(loanFEId);



            if (payment == null)
            {
                return NotFound();
            }

            return Ok(new { payment = payment });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Payment payment)
        {

            var loanList = _loanService.GetLoans().ToList();
            var loan = loanList.FirstOrDefault(l => l.FEId == payment.LoanId.ToString());

            payment.CreatedByUser = User.Identity.Name;
            payment.CreatedOnDate = DateTime.Now;
            payment.LoanId = loan.Id;

            if (ModelState.IsValid)
            {

                _paymentService.InsertPayment(payment);
                payment.LoanId = int.Parse(loan.FEId);
                return new CreatedAtRouteResult("CreatedPayment", new { id = payment.Id }, new { payment = payment });



            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Payment payment)
        {
            var loanList = _loanService.GetLoans().ToList();
            var loanId = loanList.FirstOrDefault(l => l.FEId == payment.LoanId.ToString()).Id;

            payment.CreatedByUser = User.Identity.Name;
            payment.CreatedOnDate = DateTime.Now;
            payment.LoanId = loanId;

            var paymentUpdated = _paymentService.UpdatePayment(payment);
            return Ok(new { payment = paymentUpdated });
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var payment = _paymentService.GetPaymentById(id);

            if (payment == null)
            {
                return NotFound();
            }

            _paymentService.DeletePayment(payment);
            return Ok(new { payment = payment });
        }
    }
}
