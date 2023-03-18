using FluentNHibernate.Mapping;
using Numerology.Domain.Models;

namespace Numerology.Repository.Mappings
{
    public class ClientModelMap : ClassMap<ClientModel>
    {
        public ClientModelMap()
        {
            Table("Klienci");
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Name).Column("imie");
            Map(e => e.Surname).Column("nazwisko");
            Map(e => e.BirthDate).Column("data_urodzenia");
            Map(e => e.Active).Column("active");
            Map(e => e.EntryDate).Column("data_dodania");
            Map(e => e.Telephone).Column("telefon");
            Map(e => e.Skype).Column("skype");
            Map(e => e.Email).Column("email");
            Map(e => e.Note).Column("notatka");
        }
    }
}
