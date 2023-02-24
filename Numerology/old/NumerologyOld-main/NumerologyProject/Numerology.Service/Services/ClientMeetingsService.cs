using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Repository.Interfaces;
using Numerology.Application.Interfaces;
using Numerology.Domain.Models;

namespace Numerology.Application.Services
{
    public class ClientMeetingsService : Common.Service.Service<ClientMeetings>, IClientMeetingsService
    {
        public ClientMeetingsService(IRepository<ClientMeetings> repository) : base(repository)
        {
        }

        public async override Task AddOrUpdate(ClientMeetings entity)
        {
            if(entity.Id != 0)
            {
                var entityDb = await this.Get(entity.Id);
                entity.EntryDate = entityDb.EntryDate;
            }

            await base.AddOrUpdate(entity);
        }

        public async Task<IList<ClientMeetings>> GetMeetingsBetweenDates(DateTime from, DateTime to)
        {
            return await GetList(x => x.MeetingDate >= from && x.MeetingDate <= to);
        }
    }
}
