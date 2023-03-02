using Common.Service.Interfaces;
using Numerology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Numerology.Application.Interfaces
{
    public interface IClientMeetingsService : IService<ClientMeetings>
    {
        Task<IList<ClientMeetings>> GetMeetingsBetweenDates(DateTime from, DateTime to);
    }
}
