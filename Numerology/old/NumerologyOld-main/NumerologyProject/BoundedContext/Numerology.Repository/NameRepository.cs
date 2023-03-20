using Common.UOW;
using Numerology.Domain.Models;

namespace Numerology.Repository
{
    public class NameRepository : Common.Repository.Repository<NameModel>, Common.Repository.Interfaces.IRepository<NameModel>
    {
        public NameRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
