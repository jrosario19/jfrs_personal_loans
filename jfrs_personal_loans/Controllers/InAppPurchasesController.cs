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
        [Route("verifyorderexpiration")]
        public IActionResult VerifyOrderExpiration([FromBody] InAppPurchase inAppPurchase)
        {


            

            if (ModelState.IsValid)
            {

                var scopes = new[] { AndroidPublisherService.Scope.Androidpublisher };

                ServiceAccountCredential credential;

                using (var stream = new FileStream("key.json", FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                                                     .CreateScoped(scopes)
                                                     .UnderlyingCredential as ServiceAccountCredential;


                }

                AndroidPublisherService androidPublisher = new AndroidPublisherService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Dash Clicker Juan Rosario",
                });

                MonetizationResource monetizationResource = new MonetizationResource(androidPublisher);

                InappproductsResource inappproductsResource = new InappproductsResource(androidPublisher);

                var listprod = inappproductsResource.List("jrs.personalloans.jrs_personal_loans").Execute();

                var listsub = monetizationResource.Subscriptions.List("jrs.personalloans.jrs_personal_loans").Execute();

                //var purchase = androidPublisher.Purchases.Subscriptions.Get("jrs.dashclicker", "dash_subscription_doubler", "fijdbilcdfjghmcibcmlfohg.AO-J1OwC_rdoNBGN-aJm873gNkIbLx0rQvEfaIeS47s0-GInEZjn5empw_4coPCyFFbl5BjKVXKYNJzlEyfqs80VjVHOxF4gYA").Execute();
                var purchase = androidPublisher.Purchases.Subscriptions.Get("jrs.personalloans.jrs_personal_loans", "10_us_1_mes", "iahokldldhdkfngpmijglhfm.AO-J1Ox82olS39fOiYGWTkA0A40Q4k_s0aSn82-08kq-dH2AcUZy6m1Ozw-krD28GOzz3vkBgWYa8TaULFRP_vGifhn1cav-0AbY3WMqRJQ-29t285ilq5Q").Execute();
                int i = 1 + 1;
                //AndroidPublisherService androidPublisher=new AndroidPublisherService();
                double ticks = double.Parse(purchase.ExpiryTimeMillis.ToString());
                TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                DateTime expiredate = new DateTime(1970, 1, 1) + time;
                //Console.WriteLine(expiredate.Ticks);
                //Console.WriteLine(DateTime.Now.AddHours(4).Ticks);
                //Console.WriteLine(DateTime.Now.AddHours(4).Ticks - expiredate.Ticks);
                if (expiredate.Ticks < DateTime.Now.AddHours(4).Ticks)
                {
                    return Ok(new { message = "Order is passed due", expirationdate = expiredate.AddHours(-4) });
                }
                else {
                    return Ok(new { message = "Order is current", expirationdate = expiredate.AddHours(-4) });
                }
                //Console.WriteLine(expiredate);

                //Console.WriteLine(listsub.Subscriptions[0].ProductId);

                //Console.ReadLine();
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
