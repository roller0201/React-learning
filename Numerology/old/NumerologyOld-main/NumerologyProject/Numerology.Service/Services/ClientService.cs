using Common.Repository.Interfaces;
using Numerology.Domain.Models;
using Numerology.Application.Interfaces;
using System.Threading.Tasks;
using System;

namespace Numerology.Application.Services
{
    public class ClientService : Common.Service.Service<ClientModel>, IClientService
    {
        public ClientService(IRepository<ClientModel> repository) : base(repository)
        {
        }

        public override Task AddOrUpdate(ClientModel entity)
        {
            if (entity.Id == 0)
                entity.EntryDate = DateTime.Now;

            return base.AddOrUpdate(entity);
        }
    }
}
