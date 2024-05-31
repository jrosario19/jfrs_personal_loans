using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.Clients;
using jfrs_personal_loans.Services.Installments;
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
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILoanService _loanService;
        private readonly IInstallmentService _installmentService;
        private readonly IPaymentService _paymentService;
        private readonly ITenantService _tenantService;

        public ClientsController(IClientService clientService, ILoanService loanService, IInstallmentService installmentService,
            IPaymentService paymentService,ITenantService tenantService)
        {
            this._clientService = clientService;
            this._loanService = loanService;
            this._installmentService = installmentService;
            this._paymentService = paymentService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var inactiveClientsToBeDeleteFromDB = _clientService.GetClients().Where(c => c.IsActive == false).ToList();
            var activeClientsToBeUpdated = _clientService.GetClients().Where(c => c.IsActive == true).ToList();
            var inactiveLoanToBeDeleteFromDB = _loanService.GetLoans().Where(c => c.IsActive == false).ToList();
            var activeLoanToBeUpdated = _loanService.GetLoans().Where(c => c.IsActive == true).ToList();
            var inactiveInstallmentsToBeDeleteFromDB = _installmentService.GetInstallments().Where(c => c.IsActive == false).ToList();
            var inactivePaymentsToBeDeleteFromDB = _paymentService.GetPayments().Where(c => c.IsActive == false).ToList();

            

            

            foreach (var client in inactiveClientsToBeDeleteFromDB)
            {
                _clientService.DeleteClient(client);
            }

            foreach (var loan in inactiveLoanToBeDeleteFromDB)
            {
                _loanService.DeleteLoan(loan);
            }

            foreach (var installment in inactiveInstallmentsToBeDeleteFromDB)
            {
                _installmentService.DeleteInstallment(installment);
            }

            foreach (var payment in inactivePaymentsToBeDeleteFromDB)
            {
                _paymentService.DeletePayment(payment);
            }



            var clients = _clientService.GetClients().Where(c=>c.IsActive==true).ToList();
            return Ok(new { clients = clients });
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var client = _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(new { client = client });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Client client)
        {
           
            
            client.CreatedByUser = User.Identity.Name;
            client.CreatedOnDate = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                
                    _clientService.InsertClient(client);
                return Ok(new { client = client });



            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Client client)
        {

            
            client.CreatedByUser = User.Identity.Name;
            client.CreatedOnDate = DateTime.Now;
            var clientUpdated = _clientService.UpdateClient(client);
            return Ok(new { client = clientUpdated });
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var client = _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            _clientService.DeleteClient(client);
            return Ok(new { client = client });
        }
    }
}
