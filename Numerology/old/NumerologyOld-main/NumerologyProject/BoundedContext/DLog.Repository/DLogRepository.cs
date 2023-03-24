using Common.UOW;
using DLog.Domain.Model;

namespace DLog.Repository
{
    public class DLogRepository : Common.Repository.Repository<DLogModel>, Common.Repository.Interfaces.IWriteRepository<DLogModel>
    {
        public DLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
