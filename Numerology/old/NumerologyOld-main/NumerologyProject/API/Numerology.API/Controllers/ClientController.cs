using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.UOW;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IUnitOfWork __uow;
        private readonly ILogger logger;
        public ClientController(IClientService _clientService, IUnitOfWork _uow, ILoggerFactory _loggerFactory)
        {
            clientService = _clientService;
            __uow = _uow;
            logger = _loggerFactory.CreateLogger<ClientController>();
        }

        [HttpPost("addOrUpdateClient")]
        public async Task<BaseResponse> AddOrUpdateClient(AddOrUpdateClientRequest request)
        {
            try
            {
                await clientService.AddOrUpdate(Mapper.ToClientModel(request));

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

        [HttpGet("clients/{page}/{countOnPage}")]
        public async Task<IList<ClientViewModel>> GetClients(int page, int countOnPage)
        {
            try
            {
                logger.LogError("Jestem tutaj");
                //TODO: Check how this paging exactly works
                var clients = await clientService.GetList(x => x.Id > 0 && x.Active, page, countOnPage);

                return Mapper.ToClientViewModelList(clients);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("clientsAll")]
        public async Task<IList<ClientViewModel>> GetAllClients()
        {
            try
            {
                //TODO: Check how this paging exactly works
                var clients = await clientService.GetList(x => x.Id > 0 && x.Active);

                return Mapper.ToClientViewModelList(clients.OrderBy(x => x.Name).ThenBy(x => x.Surname).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}