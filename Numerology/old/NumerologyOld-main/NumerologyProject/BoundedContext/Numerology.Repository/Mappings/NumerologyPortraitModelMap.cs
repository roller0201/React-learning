using FluentNHibernate.Mapping;
using Numerology.Domain.Models;

namespace Numerology.Repository.Mappings
{
    public class NumerologyPortraitModelMap : ClassMap<NumerologyPortraitModel>
    {
        public NumerologyPortraitModelMap()
        {
            Table("Numerology_portrait");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.AddedNames).Column("dodane_imiona");
            Map(x => x.ClientId).Column("id_osoby");
            Map(x => x.Note).Column("notatka");
            Map(x => x.SaveTime).Column("czas_zapisu");
            Map(x => x.BaseNames).Column("bazowe_imie_nazwisko");
            Map(x => x.BirthDay).Column("data_uro");
        }
    }
}
