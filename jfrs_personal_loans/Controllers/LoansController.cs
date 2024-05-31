using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.Clients;
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
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IClientService _clientService;
        private readonly ITenantService _tenantService;

        public LoansController(ILoanService loanService, IClientService clientService, ITenantService tenantService)
        {
            this._loanService = loanService;
            this._clientService = clientService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var loans = _loanService.GetLoans().Where(l=>l.IsActive==true).ToList();
            var clients = _clientService.GetClients().Where(l => l.IsActive == true).ToList();
            List<Loan> loansToBeSendToFE = new List<Loan>();
            foreach (var loan in loans)
            {
                var client = clients.FirstOrDefault(l => l.Id == loan.ClientId);
                loan.ClientId = int.Parse(client.FEId);
                loansToBeSendToFE.Add(loan);
            }
            return Ok(new { loans = loansToBeSendToFE });
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var loan = _loanService.GetLoanById(id);

            if (loan == null)
            {
                return NotFound();
            }

            return Ok(new { loan = loan });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Loan loan)
        {


            var clientList = _clientService.GetClients().ToList();
            var client = clientList.FirstOrDefault(l => l.FEId == loan.ClientId.ToString());
            loan.ClientId = client.Id;

            loan.CreatedByUser = User.Identity.Name;
            loan.CreatedOnDate = DateTime.Now;

            if (ModelState.IsValid)
            {


                var loanExist = _loanService.GetLoans().FirstOrDefault(l => l.FEId == loan.FEId);
                if (loanExist==null)
                {
                    _loanService.InsertLoan(loan);
                    loan.ClientId = int.Parse(client.FEId);
                    return Ok(new { loan = loan });
                }

                loanExist.ClientId = int.Parse(client.FEId);
                return Ok(new { loan = loanExist });
            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Loan loan)
        {


            loan.CreatedByUser = User.Identity.Name;
            loan.CreatedOnDate = DateTime.Now;
            var loanUpdated = _loanService.UpdateLoan(loan);
            return Ok(new { loan = loanUpdated });
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var loan = _loanService.GetLoanById(id);

            if (loan == null)
            {
                return NotFound();
            }

            _loanService.DeleteLoan(loan);
            return Ok(new { loan = loan });
        }
    }
}
