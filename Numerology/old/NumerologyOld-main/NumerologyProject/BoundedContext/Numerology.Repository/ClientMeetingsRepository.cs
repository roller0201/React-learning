using Common.UOW;
using Numerology.Domain.Models;

namespace Numerology.Repository
{
    public class ClientMeetingsRepository : Common.Repository.Repository<ClientMeetings>, Common.Repository.Interfaces.IRepository<ClientMeetings>
    {
        public ClientMeetingsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
