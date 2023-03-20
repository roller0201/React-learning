using FluentNHibernate.Mapping;
using Numerology.Domain.Models;

namespace Numerology.Repository.Mappings
{
    public class NameModelMap : ClassMap<NameModel>
    {
        public NameModelMap()
        {
            Table("Imiona");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Column("imie");
        }
    }
}
