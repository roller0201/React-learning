using System;

namespace Numerology.Domain.Models
{
    public class ClientModel : Common.DomainBase.IBaseEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime BirthDate { get; set; }

        public virtual bool Active { get; set; }

        //TODO: We need to add base numbers for client?
        public virtual string Telephone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Skype { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string Note { get; set; }
    }
}
