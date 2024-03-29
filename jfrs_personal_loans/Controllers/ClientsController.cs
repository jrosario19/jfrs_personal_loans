﻿using System;
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
        public IActionResult GetAll()
        {
            var clients = _clientService.GetClients().ToList();
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
