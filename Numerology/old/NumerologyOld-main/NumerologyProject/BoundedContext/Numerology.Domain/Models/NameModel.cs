using Common.DomainBase;

namespace Numerology.Domain.Models
{
    public class NameModel : IBaseEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
    }
}
