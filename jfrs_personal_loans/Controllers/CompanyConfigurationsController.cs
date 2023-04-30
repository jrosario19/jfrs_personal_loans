using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services.CompanyConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jfrs_personal_loans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyConfigurationsController : ControllerBase
    {
        private readonly ICompanyConfigurationService _companyConfigurationService;

        public CompanyConfigurationsController(ICompanyConfigurationService companyConfigurationService)
        {
            this._companyConfigurationService = companyConfigurationService;
        }

        [HttpGet]
        public IEnumerable<CompanyConfiguration> GetAll()
        {
            return _companyConfigurationService.GetCompanyConfigurations().ToList();
        }

        [HttpGet("{id}", Name ="CreatedCompanyConfiguration")]
        public IActionResult GetById(int id)
        {
            var companyConfiguration = _companyConfigurationService.GetCompanyConfigurationById(id);

            if (companyConfiguration == null)
            {
                return NotFound();
            }

            return Ok(companyConfiguration);
        }

        [HttpPost]
        public IActionResult Create ([FromBody] CompanyConfiguration companyConfiguration)
        {
            companyConfiguration.IsActive = true;
            companyConfiguration.CreatedByUser = User.Identity.Name;
            companyConfiguration.CreatedOnDate = DateTime.Now;
            //companyConfiguration.TenantId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _companyConfigurationService.InsertCompanyConfiguration(companyConfiguration);
                return new CreatedAtRouteResult("CreatedCompanyConfiguration", new { id = companyConfiguration.Id }, companyConfiguration);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CompanyConfiguration companyConfiguration, int id)
        {
            companyConfiguration.IsActive = true;
            companyConfiguration.CreatedByUser = User.Identity.Name;
            companyConfiguration.CreatedOnDate = DateTime.Now;
            //companyConfiguration.TenantId = User.Identity.Name;
            if (companyConfiguration.Id!=id)
            {
                return BadRequest();
            }

            _companyConfigurationService.UpdateCompanyConfiguration(companyConfiguration);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var companyConfiguration = _companyConfigurationService.GetCompanyConfigurationById(id);
            
            if (companyConfiguration==null)
            {
                return NotFound();
            }

            _companyConfigurationService.DeleteCompanyConfiguration(companyConfiguration);
            return Ok(companyConfiguration);
        }
    }
}