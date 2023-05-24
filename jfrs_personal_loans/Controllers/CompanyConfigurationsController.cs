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
        [Route("getall")]
        public IEnumerable<CompanyConfiguration> GetAll()
        {
            return _companyConfigurationService.GetCompanyConfigurations().ToList();
        }

        [HttpGet("{id}", Name ="CreatedCompanyConfiguration")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var companyConfiguration = _companyConfigurationService.GetCompanyConfigurationById(id);

            if (companyConfiguration == null)
            {
                return NotFound();
            }

            return Ok(new { companyConfiguration = companyConfiguration });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create ([FromBody] CompanyConfiguration companyConfiguration)
        {
            companyConfiguration.IsActive = true;
            companyConfiguration.CreatedByUser = User.Identity.Name;
            companyConfiguration.CreatedOnDate = DateTime.Now;
            //companyConfiguration.TenantId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _companyConfigurationService.InsertCompanyConfiguration(companyConfiguration);
                return new CreatedAtRouteResult("CreatedCompanyConfiguration", new { id = companyConfiguration.Id }, new { companyConfiguration = companyConfiguration } );
            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] CompanyConfiguration companyConfiguration)
        {
            
            //var companyConfigurationToBeUpdated = _companyConfigurationService.GetCompanyConfigurationById(companyConfiguration.Id);
            //companyConfiguration.TenantId = User.Identity.Name;
            //if (companyConfigurationToBeUpdated == null)
            //{

            //    ModelState.AddModelError("message", "Invalid input attempt.");
            //    return BadRequest(ModelState);
            //}
            companyConfiguration.IsActive = true;
            companyConfiguration.CreatedByUser = User.Identity.Name;
            companyConfiguration.CreatedOnDate = DateTime.Now;
            var companyConfigurationUpdated= _companyConfigurationService.UpdateCompanyConfiguration(companyConfiguration);
            return Ok(new { companyConfiguration = companyConfigurationUpdated });
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var companyConfiguration = _companyConfigurationService.GetCompanyConfigurationById(id);
            
            if (companyConfiguration==null)
            {
                return NotFound();
            }

            _companyConfigurationService.DeleteCompanyConfiguration(companyConfiguration);
            return Ok(new { companyConfiguration = companyConfiguration });
        }
    }
}