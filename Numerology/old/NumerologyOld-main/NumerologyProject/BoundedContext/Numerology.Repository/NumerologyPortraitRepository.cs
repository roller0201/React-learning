using Common.UOW;
using Numerology.Domain.Models;

namespace Numerology.Repository
{
    public class NumerologyPortraitRepository : Common.Repository.Repository<NumerologyPortraitModel>, Common.Repository.Interfaces.IRepository<NumerologyPortraitModel>
    {
        public NumerologyPortraitRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
