using Common.Repository.Interfaces;
using Numerology.Domain.Models;
using Numerology.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Numerology.Application.Services
{
    public class NameService : Common.Service.Service<NameModel>, INameService
    {
        public NameService(IRepository<NameModel> repository) : base(repository)
        {
        }

        public async Task RemoveAsync(int id)
        {
            var element = await Get(id);

            await RemoveAsync(element);
        }

        public async Task RemoveAsync(IList<int> ids)
        {
            var elements = await GetList(x => ids.Contains(x.Id));

            await RemoveAsync(elements);
        }
    }
}
