using System;

namespace Numerology.Domain.Models
{
    public class ClientMeetings : Common.DomainBase.IBaseEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual int ClientId { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual DateTime MeetingDate { get; set; }
        public virtual int MettingHour { get; set; }
        public virtual int MettingMinute { get; set; }
        public virtual int MettingDurationHour { get; set; }
        public virtual int MettingDurationMinute { get; set; }
        public virtual string Note { get; set; }

    }
}
