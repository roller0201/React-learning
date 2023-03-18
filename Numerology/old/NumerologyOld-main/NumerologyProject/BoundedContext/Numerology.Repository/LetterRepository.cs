using Common.UOW;
using Numerology.Domain.Models;

namespace Numerology.Repository
{
    public class LetterRepository : Common.Repository.Repository<LetterModel>, Common.Repository.Interfaces.IRepository<LetterModel>
    {
        public LetterRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
