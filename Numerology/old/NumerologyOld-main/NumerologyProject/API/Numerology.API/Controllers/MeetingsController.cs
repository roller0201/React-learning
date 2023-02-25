using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Numerology.API.Helpers;
using Numerology.API.ViewModels;
using Numerology.API.ViewModels.Models.Client;
using Numerology.API.ViewModels.RequestViewModel.Client;
using Numerology.Application.Interfaces;

namespace Numerology.API.Controllers
{
    //[EnableCors("AllowAllCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IClientMeetingsService clientMeetingsService;

        public MeetingsController(IClientMeetingsService _clientMeetingsService)
        {
            clientMeetingsService = _clientMeetingsService;
        }

        [HttpGet("meetings/{from}/{to}")]
        public async Task<IList<ClientMeetingsViewModel>> GetMeetings(DateTime from, DateTime to)
        {
            try
            {
                var meetings = await clientMeetingsService.GetMeetingsBetweenDates(from, to);

                var groupByDate = meetings.OrderBy(x => x.MeetingDate).GroupBy(x => x.MeetingDate, (key, g) => new { Date = key, Values = g.ToList() });

                List<ClientMeetingsViewModel> result = new List<ClientMeetingsViewModel>();

                foreach(var group in groupByDate)
                {
                    result.AddRange(group.Values.Select(x => Mapper.ToClientMeetingsViewModel(x)));
                }

                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("meetings")]
        public async Task<IList<ClientMeetingsViewModel>> GetMeetings()
        {
            try
            {
                var meetings = await clientMeetingsService.GetList(x => x.Id > 0);

                var groupByDate = meetings.OrderBy(x => x.MeetingDate).GroupBy(x => x.MeetingDate, (key, g) => new { Date = key, Values = g.ToList() });

                List<ClientMeetingsViewModel> result = new List<ClientMeetingsViewModel>();

                foreach (var group in groupByDate)
                {
                    result.AddRange(group.Values.Select(x => Mapper.ToClientMeetingsViewModel(x)));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("addOrUpdateMeeting")]
        public async Task<BaseResponse> AddOrUpdateMeeting(ClientMeetingsViewModel model)
        {
            try
            {
                await clientMeetingsService.AddOrUpdate(Mapper.ToClientMeetings(model));

                return new BaseResponse
                {
                    Success = true,
                    Message = new List<string>()
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = new List<string>() { ex.ToString() }
                };
            }
        }

        [HttpPost("delete")]
        public async Task<BaseResponse> Delete(DeleteMeetingRequest request)
        {
            try
            {
                var entity = await clientMeetingsService.Get(request.Id);

                if(entity != null)
                {
                    await clientMeetingsService.RemoveAsync(entity);

                    return new BaseResponse
                    {
                        Success = true,
                        Message = new List<string>()
                    };
                }

                return new BaseResponse
                {
                    Success = false,
                    Message = new List<string>() { $"Nie udalo sie znalezc rekordu o id: {request.Id}" }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = new List<string>() { ex.ToString() }
                };
            }
        }
    }
}