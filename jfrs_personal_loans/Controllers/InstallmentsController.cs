using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.Installments;
using jfrs_personal_loans.Services.Loans;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jfrs_personal_loans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InstallmentsController : ControllerBase
    {
        private readonly IInstallmentService _installmentService;
        private readonly ILoanService _loanService;
        private readonly ITenantService _tenantService;

        public InstallmentsController(IInstallmentService installmentService, ILoanService loanService,ITenantService tenantService)
        {
            this._installmentService = installmentService;
            this._loanService = loanService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var installments = _installmentService.GetInstallments().ToList();
            List<Installment> newInstallments = new List<Installment>();


            foreach (var installment1 in installments)
            {
                var installment = _installmentService.GetInstallmentById(installment1.Id);
                var loanList = _loanService.GetLoans().ToList();
                var loanFEId = loanList.FirstOrDefault(l => l.Id == installment.LoanId).FEId;
                installment.LoanId = int.Parse(loanFEId);
                newInstallments.Add(installment);
            }

            return Ok(new { installments = newInstallments });
        }

        [HttpGet("{id}", Name = "CreatedInstallment")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var installment = _installmentService.GetInstallmentById(id);
            var loanList = _loanService.GetLoans().ToList();
            var loanFEId = loanList.FirstOrDefault(l => l.Id == installment.LoanId).FEId;
            installment.LoanId = int.Parse(loanFEId);




            if (installment == null)
            {
                return NotFound();
            }

            return Ok(new { installment = installment });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Installment installment)
        {

            var loanList = _loanService.GetLoans().ToList();
            var loanId = loanList.FirstOrDefault(l => l.FEId == installment.LoanId.ToString()).Id;

            installment.CreatedByUser = User.Identity.Name;
            installment.CreatedOnDate = DateTime.Now;
            installment.LoanId = loanId;

            if (ModelState.IsValid)
            {

                _installmentService.InsertInstallment(installment);
                return new CreatedAtRouteResult("CreatedInstallment", new { id = installment.Id }, new { installment = installment });



            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Installment installment)
        {
            var loanList = _loanService.GetLoans().ToList();
            var loanId = loanList.FirstOrDefault(l => l.FEId == installment.LoanId.ToString()).Id;
            installment.LoanId = loanId;

            installment.CreatedByUser = User.Identity.Name;
            installment.CreatedOnDate = DateTime.Now;
            var installmentUpdated = _installmentService.UpdateInstallment(installment);
            return Ok(new { installment = installmentUpdated });
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var installment = _installmentService.GetInstallmentById(id);

            if (installment == null)
            {
                return NotFound();
            }

            _installmentService.DeleteInstallment(installment);
            return Ok(new { installment = installment });
        }
    }
}
