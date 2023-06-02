using jfrs_personal_loans.Data;
using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Client InsertClient(Client Client)
        {
            Client client = _unitOfWork.ClientRepository.Insert(Client);
            _unitOfWork.SaveChanges();
            return client;
        }
        public Client GetClientById(int? Id)
        {
            return _unitOfWork.ClientRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();
        }

        public IQueryable<Client> GetClients()
        {
            return _unitOfWork.ClientRepository.GetAll();
        }
        public Client UpdateClient(Client Client)
        {
            Client client = _unitOfWork.ClientRepository.Update(Client);
            _unitOfWork.SaveChanges();
            return client;
        }
        public Client DeleteClient(Client Client)
        {
            Client.IsActive = false;
            Client client = _unitOfWork.ClientRepository.Update(Client);
            _unitOfWork.SaveChanges();
            return client;
            //return _ClientRepository.Delete(Client);
        }
    }
}
