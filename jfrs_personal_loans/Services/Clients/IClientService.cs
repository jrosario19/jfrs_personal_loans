using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Clients
{
    public interface IClientService
    {
        Client InsertClient(Client Client);
        IQueryable<Client> GetClients();

        Client GetClientById(int? Id);
        Client UpdateClient(Client Client);
        Client DeleteClient(Client Client);
    }
}
