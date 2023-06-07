using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.Installments;
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
        private readonly ITenantService _tenantService;

        public InstallmentsController(IInstallmentService installmentService, ITenantService tenantService)
        {
            this._installmentService = installmentService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var installments = _installmentService.GetInstallments().ToList();
            return Ok(new { installments = installments });
        }

        [HttpGet("{id}", Name = "CreatedInstallment")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var installment = _installmentService.GetInstallmentById(id);

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


            installment.CreatedByUser = User.Identity.Name;
            installment.CreatedOnDate = DateTime.Now;

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
