using Common.Repository.Interfaces;
using Numerology.Domain.Models;
using Numerology.Application.Interfaces;

namespace Numerology.Application.Services
{
    public class NumerologyPortraitService : Common.Service.Service<NumerologyPortraitModel>, INumerologyPortraitService
    {
        public NumerologyPortraitService(IRepository<NumerologyPortraitModel> repository) : base(repository)
        {
        }
    }
}
