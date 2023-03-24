using Common.DomainBase;
using System;

namespace DLog.Domain.Model
{
    public class DLogModel : IBaseEntity<string>
    {
        public string Id { get; set; }
        public int LogType { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public string Who { get; set; }
    }
}
