using Common.Service.Interfaces;
using Numerology.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Numerology.Application.Interfaces
{
    public interface INameService : IService<NameModel>
    {
        Task RemoveAsync(int id);
        Task RemoveAsync(IList<int> ids);
    }
}
