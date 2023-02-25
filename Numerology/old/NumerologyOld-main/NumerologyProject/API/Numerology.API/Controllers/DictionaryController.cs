using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.UOW;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Numerology.API.Helpers;
using Numerology.API.ViewModels;
using Numerology.API.ViewModels.Models.Dictionary;
using Numerology.API.ViewModels.RequestViewModel.Dictionary;
using Numerology.Application.Interfaces;

namespace Numerology.API.Controllers
{
    //https://medium.com/@talaviya.bhavdip/how-to-upload-file-in-angular-with-asp-net-core-2-1-46caed6d062a
   //[EnableCors("AllowAllCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly INameService nameService;
        private readonly ILetterService letterService;
        private readonly IUnitOfWork __uow;

        public DictionaryController(INameService _nameService, ILetterService _letterService, IUnitOfWork _uow)
        {
            nameService = _nameService;
            letterService = _letterService;
            __uow = _uow;
        }

        #region Name

        [HttpPost("addOrUpdateName")]
        public async Task<BaseResponse> AddOrUpdateName(AddToDictionaryRequest request)
        {
            try
            {
                foreach(var item in request.Names)
                {
                    await nameService.AddOrUpdate(Mapper.ToNameModel(item));
                }

                return new BaseResponse
                {
                    Success = true,
                    Message = new List<string>()
                };
            }
            catch(Exception ex)
            {
                __uow.Rollback();
                return new BaseResponse
                {
                    Success = false,
                    Message = new List<string>() { ex.ToString() }
                };
            }
        }

        [HttpGet("names/{page}/{countOnPage}")]
        public async Task<IList<DictionaryViewModel>> GetNameDictionaryValues(/*GetDictionaryRequest request*/ int page, int countOnPage)
        {
            try
            {
                //TODO: Check how this paging exactly works
                //var names = await nameService.GetList(x => x.Id > 0, request.Page, request.CountOnPage);
                var names = await nameService.GetList(x => x.Id > 0, page, countOnPage);

                return Mapper.ToDictionaryViewModelList(names);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("namesAll")]
        public async Task<IList<DictionaryViewModel>> GetNameDictionaryValues()
        {
            try
            {
                //TODO: Check how this paging exactly works
                //var names = await nameService.GetList(x => x.Id > 0, request.Page, request.CountOnPage);
                var names = await nameService.GetList(x => x.Id > 0);

                return Mapper.ToDictionaryViewModelList(names);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("names/delete")]
        public async Task<BaseResponse> DeleteNames(DeleteFromDictionaryRequest request)
        {
            try
            {
                await nameService.RemoveAsync(request.ToDelete.Select(x => x.Id).ToList());

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

        #endregion

        #region Letter

        [HttpPost("addOrUpdateLetter")]
        public async Task<BaseResponse> AddOrUpdateLetter(AddToDictionaryRequest request)
        {
            try
            {
                foreach (var item in request.Names)
                {
                    await letterService.AddOrUpdate(Mapper.ToLetterModel(item));
                }

                return new BaseResponse
                {
                    Success = true,
                    Message = new List<string>()
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

        [HttpGet("letters")]
        public async Task<IList<DictionaryViewModel>> GetLetterDictionaryValues(GetDictionaryRequest request)
        {
            try
            {
                //TODO: Check how this paging exactly works
                var letters = await letterService.GetList(x => x.Id > 0, request.Page, request.CountOnPage);

                return Mapper.ToDictionaryViewModelList(letters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("lettersAll")]
        public async Task<IList<DictionaryViewModel>> GetLetterDictionaryValues()
        {
            try
            {
                //TODO: Check how this paging exactly works
                var letters = await letterService.GetList(x => x.Id > 0);

                return Mapper.ToDictionaryViewModelList(letters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("letters/delete")]
        public async Task<BaseResponse> DeleteLetters(DeleteFromDictionaryRequest request)
        {
            try
            {
                //RemoveAsync should throw error if could not find element to delete
                await letterService.RemoveAsync(request.ToDelete.Select(x => x.Id).ToList());

                return new BaseResponse
                {
                    Success = true,
                    Message = new List<string>()
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

        #endregion
    }
}