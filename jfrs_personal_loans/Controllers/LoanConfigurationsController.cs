using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
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
        private readonly ITenantService _tenantService;

        public LoanConfigurationsController(ILoanConfigurationService loanConfigurationService, ITenantService tenantService)
        {
            this._loanConfigurationService = loanConfigurationService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var loanConfigurations =  _loanConfigurationService.GetLoanConfigurations().ToList();
            return Ok(new { loanConfigurations = loanConfigurations });
        }

        [HttpGet]
        [Route("getbyid/{id}")]
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
            var tenantId = _tenantService.GetTenant();
            var loanConfigurationExist = _loanConfigurationService.GetLoanConfigurations().FirstOrDefault(lc => lc.TenantId == tenantId);
            

            loanConfiguration.IsActive = true;
            loanConfiguration.CreatedByUser = User.Identity.Name;
            loanConfiguration.CreatedOnDate = DateTime.Now;
            //loanConfiguration.TenantId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (loanConfigurationExist != null)
                {
                    loanConfigurationExist.PaymentFrequency = loanConfiguration.PaymentFrequency;
                    loanConfigurationExist.InterestApplication = loanConfiguration.InterestApplication;
                    loanConfigurationExist.DefaultInterest = loanConfiguration.DefaultInterest;
                    loanConfigurationExist.IsAllowLoanArrears = loanConfiguration.IsAllowLoanArrears;
                    loanConfigurationExist.LoanArrearsApplication = loanConfiguration.LoanArrearsApplication;
                    loanConfigurationExist.LoanArrearsInterest = loanConfiguration.LoanArrearsInterest;
                    loanConfigurationExist.LoanArrearsAllowDays = loanConfiguration.LoanArrearsAllowDays;
                    loanConfigurationExist.IgnoredWeekDays = loanConfiguration.IgnoredWeekDays;
                    loanConfigurationExist.MonthDays = loanConfiguration.MonthDays;
                    loanConfigurationExist.FortnightDays = loanConfiguration.FortnightDays;
                    loanConfigurationExist.IsAllowToSetDayForPayment = loanConfiguration.IsAllowToSetDayForPayment;
                    loanConfigurationExist.PaymentWeekDay = loanConfiguration.PaymentWeekDay;
                    loanConfigurationExist.PaymentFirstFortnightDay = loanConfiguration.PaymentFirstFortnightDay;
                    loanConfigurationExist.PaymentSecondFortnightDay = loanConfiguration.PaymentSecondFortnightDay;
                    loanConfigurationExist.PaymentMonthtDay = loanConfiguration.PaymentMonthtDay;

                    loanConfigurationExist.IsActive = true;
                    loanConfigurationExist.CreatedByUser = User.Identity.Name;
                    loanConfigurationExist.CreatedOnDate = DateTime.Now;

                    _loanConfigurationService.UpdateLoanConfiguration(loanConfigurationExist);
                    return Ok(new { loanConfiguration = loanConfigurationExist });
                }
                else {
                    _loanConfigurationService.InsertLoanConfiguration(loanConfiguration);
                    return Ok(new { loanConfiguration = loanConfiguration });
                }
                
                
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

        [HttpDelete]
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