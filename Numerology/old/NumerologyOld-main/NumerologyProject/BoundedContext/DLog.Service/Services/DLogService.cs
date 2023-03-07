using Common.Repository.Interfaces;
using Common.Service;
using DLog.Domain.Model;
using DLog.Service.Interfaces;

namespace DLog.Service.Services
{
    public class DLogService : Service<DLogModel>, IDLogService
    {
        public DLogService(IRepository<DLogModel> repository) : base(repository)
        {
        }
    }
}
