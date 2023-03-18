using FluentNHibernate.Mapping;
using Numerology.Domain.Models;

namespace Numerology.Repository.Mappings
{
    public class ClientMeetingsMap : ClassMap<ClientMeetings>
    {
        public ClientMeetingsMap()
        {
            Table("Spotkania_z_klientami");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.MeetingDate).Column("data_spotkania");
            Map(x => x.MettingHour).Column("godzina_spotkania");
            Map(x => x.MettingMinute).Column("minuta_spotkania");
            Map(x => x.ClientId).Column("id_klienta");
            Map(x => x.EntryDate).Column("data_dodania");
            Map(x => x.MettingDurationHour).Column("godziny_na_spotkaniu");
            Map(x => x.MettingDurationMinute).Column("minuty_na_spotkaniu");
            Map(x => x.Note).Column("notatka");
        }
    }
}
