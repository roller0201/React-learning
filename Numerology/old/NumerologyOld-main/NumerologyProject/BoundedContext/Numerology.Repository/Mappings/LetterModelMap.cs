using FluentNHibernate.Mapping;
using Numerology.Domain.Models;

namespace Numerology.Repository.Mappings
{
    public class LetterModelMap : ClassMap<LetterModel>
    {
        public LetterModelMap()
        {
            Table("Litery");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Letter).Column("litera");
            Map(x => x.Value).Column("wartosc");
            Map(x => x.Vowel).Column("czy_samogloska");
        }
    }
}
