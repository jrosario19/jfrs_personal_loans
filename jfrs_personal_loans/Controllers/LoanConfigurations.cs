using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services.LoanConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jfrs_personal_loans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoanConfigurationsController : ControllerBase
    {
        private readonly ILoanConfigurationService _loanConfigurationService;

        public LoanConfigurationsController(ILoanConfigurationService loanConfigurationService)
        {
            this._loanConfigurationService = loanConfigurationService;
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<LoanConfiguration> GetAll()
        {
            return _loanConfigurationService.GetLoanConfigurations().ToList();
        }

        [HttpGet("{id}", Name = "CreatedLoanConfiguration")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var loanConfiguration = _loanConfigurationService.GetLoanConfigurationById(id);

            if (loanConfiguration == null)
            {
                return NotFound();
            }

            return Ok(new { loanConfiguration = loanConfiguration });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] LoanConfiguration loanConfiguration)
        {
            loanConfiguration.IsActive = true;
            loanConfiguration.CreatedByUser = User.Identity.Name;
            loanConfiguration.CreatedOnDate = DateTime.Now;
            //loanConfiguration.TenantId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _loanConfigurationService.InsertLoanConfiguration(loanConfiguration);
                return new CreatedAtRouteResult("CreatedLoanConfiguration", new { id = loanConfiguration.Id }, new { loanConfiguration = loanConfiguration });
            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] LoanConfiguration loanConfiguration)
        {

            //var loanConfigurationToBeUpdated = _loanConfigurationService.GetLoanConfigurationById(loanConfiguration.Id);
            //loanConfiguration.TenantId = User.Identity.Name;
            //if (loanConfigurationToBeUpdated == null)
            //{

            //    ModelState.AddModelError("message", "Invalid input attempt.");
            //    return BadRequest(ModelState);
            //}
            loanConfiguration.IsActive = true;
            loanConfiguration.CreatedByUser = User.Identity.Name;
            loanConfiguration.CreatedOnDate = DateTime.Now;
            var loanConfigurationUpdated = _loanConfigurationService.UpdateLoanConfiguration(loanConfiguration);
            return Ok(new { loanConfiguration = loanConfigurationUpdated });
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var loanConfiguration = _loanConfigurationService.GetLoanConfigurationById(id);

            if (loanConfiguration == null)
            {
                return NotFound();
            }

            _loanConfigurationService.DeleteLoanConfiguration(loanConfiguration);
            return Ok(new { loanConfiguration = loanConfiguration });
        }
    }
}