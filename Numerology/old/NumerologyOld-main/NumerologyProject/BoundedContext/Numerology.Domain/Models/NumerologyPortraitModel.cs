using Common.DomainBase;
using System;

namespace Numerology.Domain.Models
{
    public class NumerologyPortraitModel : IBaseEntity<Int64>
    {
        public virtual long Id { get; set; }
        public virtual string BaseNames { get; set; }
        public virtual DateTime BirthDay { get; set; }
        public virtual string AddedNames { get; set; }
        public virtual DateTime SaveTime { get; set; }
        public virtual int ClientId { get; set; }
        public virtual string Note { get; set; }
    }
}
