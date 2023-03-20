using Common.UOW;
using Numerology.Domain.Models;

namespace Numerology.Repository
{
    public class ClientRepository : Common.Repository.Repository<ClientModel>, Common.Repository.Interfaces.IRepository<ClientModel>
    {
        public ClientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
