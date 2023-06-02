using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jfrs_personal_loans.Models;
using jfrs_personal_loans.Services;
using jfrs_personal_loans.Services.Clients;
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
        private readonly ITenantService _tenantService;

        public ClientsController(IClientService clientService, ITenantService tenantService)
        {
            this._clientService = clientService;
            this._tenantService = tenantService;
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<Client> GetAll()
        {
            return _clientService.GetClients().ToList();
        }

        [HttpGet("{id}", Name = "CreatedClient")]
        [Route("getbyid")]
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
            var tenantId = _tenantService.GetTenant();
            var clientExist = _clientService.GetClients().FirstOrDefault(lc => lc.TenantId == tenantId);
            clientExist.Name = client.Name;
            clientExist.LastName = client.LastName;
            clientExist.Identification = client.Identification;
            clientExist.Code = client.Code;
            clientExist.CellPhoneNumber = client.CellPhoneNumber;
            clientExist.HomePhoneNumber = client.HomePhoneNumber;
            clientExist.Address = client.Address;
            clientExist.ReferalName = client.ReferalName;
            clientExist.ReferalPhonenumber = client.ReferalPhonenumber;
            clientExist.ReferalAddress = client.ReferalAddress;
            

            clientExist.IsActive = true;
            clientExist.CreatedByUser = User.Identity.Name;
            clientExist.CreatedOnDate = DateTime.Now;

            client.IsActive = true;
            client.CreatedByUser = User.Identity.Name;
            client.CreatedOnDate = DateTime.Now;
            //client.TenantId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (clientExist != null)
                {
                    _clientService.UpdateClient(clientExist);
                }
                else
                {
                    _clientService.InsertClient(client);
                }

                return new CreatedAtRouteResult("CreatedClient", new { id = client.Id }, new { client = client });
            }

            ModelState.AddModelError("message", "Invalid input attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Client client)
        {

            //var clientToBeUpdated = _clientService.GetClientById(client.Id);
            //client.TenantId = User.Identity.Name;
            //if (clientToBeUpdated == null)
            //{

            //    ModelState.AddModelError("message", "Invalid input attempt.");
            //    return BadRequest(ModelState);
            //}
            client.IsActive = true;
            client.CreatedByUser = User.Identity.Name;
            client.CreatedOnDate = DateTime.Now;
            var clientUpdated = _clientService.UpdateClient(client);
            return Ok(new { client = clientUpdated });
        }

        [HttpDelete("{id}")]
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
