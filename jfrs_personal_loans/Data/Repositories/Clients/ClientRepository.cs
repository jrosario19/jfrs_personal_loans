using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data.Repositories.Clients
{
    public class ClientRepository : SQLRepository<Client>, IClientRepository
    {
        public ClientRepository(JFRSPersonalLoansDBContext context) : base(context)
        {

        }
    }
}
