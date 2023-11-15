using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Services;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using System.IO;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services.InAppPurchases;
using jfrs_personal_loans.Services;

namespace jfrs_personal_loans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InAppPurchasesController : ControllerBase
    {
        private readonly IInAppPurchaseService _inAppPurchaseService;
        private readonly ITenantService _tenantService;

        public InAppPurchasesController(IInAppPurchaseService inAppPurchaseService, ITenantService tenantService)
        {
            this._inAppPurchaseService = inAppPurchaseService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var inAppPurchases = _inAppPurchaseService.GetInAppPurchases().ToList();
            return Ok(new { inAppPurchases = inAppPurchases });
        }

        [HttpGet("{id}", Name = "CreatedInAppPurchase")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var inAppPurchase = _inAppPurchaseService.GetInAppPurchaseById(id);

            if (inAppPurchase == null)
            {
                return NotFound();
            }

            return Ok(new { inAppPurchase = inAppPurchase });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] InAppPurchase inAppPurchase)
        {


            inAppPurchase.CreatedByUser = User.Identity.Name;
            inAppPurchase.CreatedOnDate = DateTime.Now;

            if (ModelState.IsValid)
            {

                _inAppPurchaseService.InsertInAppPurchase(inAppPurchase);
                return new CreatedAtRouteResult("CreatedInAppPurchase", new { id = inAppPurchase.Id }, new { inAppPurchase = inAppPurchase });



            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] InAppPurchase inAppPurchase)
        {


            inAppPurchase.CreatedByUser = User.Identity.Name;
            inAppPurchase.CreatedOnDate = DateTime.Now;
            var inAppPurchaseUpdated = _inAppPurchaseService.UpdateInAppPurchase(inAppPurchase);
            return Ok(new { inAppPurchase = inAppPurchaseUpdated });
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var inAppPurchase = _inAppPurchaseService.GetInAppPurchaseById(id);

            if (inAppPurchase == null)
            {
                return NotFound();
            }

            _inAppPurchaseService.DeleteInAppPurchase(inAppPurchase);
            return Ok(new { inAppPurchase = inAppPurchase });
        }
    }
}
