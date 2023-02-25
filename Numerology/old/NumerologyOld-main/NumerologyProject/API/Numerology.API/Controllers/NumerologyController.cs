using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Numerology.API.Enums;
using Numerology.API.Helpers;
using Numerology.API.ViewModels;
using Numerology.API.ViewModels.Models.Numerology;
using Numerology.API.ViewModels.RequestViewModel.Numerology;
using Numerology.Application;
using Numerology.Application.Interfaces;
using Numerology.Domain.Models;

namespace Numerology.API.Controllers
{
    //[EnableCors("AllowAllCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NumerologyController : ControllerBase
    {
        private readonly ILetterService letterService;
        private readonly INumerologyPortraitService numerologyPortraitService;
        private readonly INumerologyCalculator numerologyCalculator;

        public NumerologyController(ILetterService _letterService, INumerologyPortraitService _numerologyPortraitService, INumerologyCalculator _numerologyCalculator)
        {
            letterService = _letterService;
            numerologyPortraitService = _numerologyPortraitService;
            numerologyCalculator = _numerologyCalculator;
        }

        [HttpGet("numerologyName/{name}/")]
        public async Task<NumerologyNameViewModel> GetNumerologyName(string name)
        {
            var letters = await letterService.GetList(x => x.Id > 0);
            var upRowMainWhole = await this.numerologyCalculator.CalculateInsideNumber(name, letters);
            var downRowMainWhole = await this.numerologyCalculator.CalculateOutsideNumber(name, letters);
            var upRow = await this.numerologyCalculator.CalculateNameUpRow(name, letters);
            var upRowMain = await this.numerologyCalculator.CalculateNameMainRow(upRow.Split('-'));
            var downRow = await this.numerologyCalculator.CalculateNameDownRow(name, letters);
            var downRowMain = await this.numerologyCalculator.CalculateNameMainRow(downRow.Split('-'));
            var numbersEach = await this.numerologyCalculator.CalculateEachNumber(name, letters);

            return new NumerologyNameViewModel
            {
                UpRow = upRow,
                UpRowMain = upRowMain,
                UpRowMainWhole = upRowMainWhole,
                DownRow = downRow,
                DownRowMain = downRowMain,
                DownRowMainWhole = downRowMainWhole,
                NameNumber = "",
                NumerologyString = "",
                One = numbersEach["1"],
                Two = numbersEach["2"],
                Three = numbersEach["3"],
                Four = numbersEach["4"],
                Five = numbersEach["5"],
                Six = numbersEach["6"],
                Seven = numbersEach["7"],
                Eight = numbersEach["8"],
                Nine = numbersEach["9"],
            };
        }

        [HttpGet("numerologyNames/{baseName}/{names}/")]
        public async Task<NumerologyNameViewModel> GetNumerologyNames(string baseName, string names)
        {
            //Fix for api path
            if (names == "-")
                names = "";

            var nameWhole = baseName + " " + names;
            var letters = await letterService.GetList(x => x.Id > 0);
            var upRowMainWhole = await this.numerologyCalculator.CalculateInsideNumber(nameWhole, letters);
            var downRowMainWhole = await this.numerologyCalculator.CalculateOutsideNumber(nameWhole, letters);
            var upRow = await this.numerologyCalculator.CalculateNameUpRow(nameWhole, letters);
            var upRowMain = await this.numerologyCalculator.CalculateNameMainRow(upRow.Split('-'));
            var downRow = await this.numerologyCalculator.CalculateNameDownRow(nameWhole, letters);
            var downRowMain = await this.numerologyCalculator.CalculateNameMainRow(downRow.Split('-'));
            var numbersEach = await this.numerologyCalculator.CalculateEachNumber(nameWhole, letters);

            return new NumerologyNameViewModel
            {
                UpRow = upRow,
                UpRowMain = upRowMain,
                UpRowMainWhole = upRowMainWhole,
                DownRow = downRow,
                DownRowMain = downRowMain,
                DownRowMainWhole = downRowMainWhole,
                NameNumber = "",
                NumerologyString = "",
                One = numbersEach["1"],
                Two = numbersEach["2"],
                Three = numbersEach["3"],
                Four = numbersEach["4"],
                Five = numbersEach["5"],
                Six = numbersEach["6"],
                Seven = numbersEach["7"],
                Eight = numbersEach["8"],
                Nine = numbersEach["9"],
            };
        }

        [HttpGet("numerologyDate/{date}/")]
        public async Task<IActionResult> GetNumerologyDate(string date)
        {
            //var birthdateArray = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var birthdateArray = date.Split('-');
            var year = birthdateArray[0];
            var month = birthdateArray[1];
            var day = birthdateArray[2];

            //string sumString = GetDateNumerologySum(year) + GetDateNumerologySum(month) + GetDateNumerologySum(day);

            //var partialResult = GetDateNumerologySum(sumString);

            var test = await this.numerologyCalculator.CalculateNumerologyBirthDate(year + month + day);

            if(test.Count() > 2)
            {
                return Ok(new { Test = test[0] + "/" + test[1] + "/" + test[2] });
            }

            //Not sure why we cannot just return string. We must return propoer JSON object
            return Ok(new { Test = test[0] + "/" + test[1] });
            //return partialResult + "\\" + sumString;
        }

        [HttpGet("numerologyDateTree/{date}/")]
        public async Task<IActionResult> GetNumerologyDateTree(string date)
        {
            var splitedDate = date.Split('-');

            return Ok(await this.numerologyCalculator.CalculateTreeBirthDate(splitedDate[0], splitedDate[1], splitedDate[2]));
        }


        [HttpGet("portrait/{id}")]
        public async Task<IList<NumerologyPortraitViewModel>> GetNumerologyPortraits(int id)
        {
            var portraits = await numerologyPortraitService.GetList(x => x.ClientId == id);

            return Mapper.ToNumerologyPortraitViewModelList(portraits);
        }

        [HttpPost("addPortrait")]
        public async Task<BaseResponse> AddNewNumerologyPortrait(NumerologyPortraitViewModel model)
        {
            var test = Mapper.ToNumerologyPortraitModel(model); // Check output
            await numerologyPortraitService.AddOrUpdate(test);

            return new BaseResponse { Message = new string[] { "" }, Success = true };
        }

        [HttpPost("printPortrait")]
        public async Task<IActionResult> CreatePdfAndGetName(PrintRequest printRequest)
        {
            var portraits = await numerologyPortraitService.GetList(x => x.Id == printRequest.PortraitId);
            var portrait = portraits.FirstOrDefault();

            if (portrait == null)
                throw new Exception($"Could not find portrait in database with id: {printRequest.PortraitId}");

            var fileName = await NumerologyPrinter.CreateDocument(printRequest.BaseNameModel, printRequest.AddedNameModel, printRequest.WholeNameModel, portrait);


            return Ok(new { Name = fileName });
        }


        [HttpGet("getPrint/{fileName}/")]
        public IActionResult DownloadFile(string fileName)
        {
            var path = Directory.GetCurrentDirectory() + "\\pdfs\\" + fileName + ".pdf";


            //var memory = new MemoryStream();
            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}

            //memory.Position = 0;
            //return File(memory, GetMimeType(path), fileName);

            var data = System.IO.File.ReadAllBytes(path);
            return File(data, MediaTypeNames.Application.Pdf, fileName);

            //return new FileStream(path, FileMode.Open, FileAccess.Read);
            //return new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        [HttpDelete("deletePortrait/{id}/")]
        public async Task<IActionResult> DeletePortrait(long id)
        {
            var entity = await this.numerologyPortraitService.GetList(x => x.Id == id);

            await this.numerologyPortraitService.RemoveAsync(entity.FirstOrDefault());

            return Ok();
        }

        private string GetMimeType(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }
    }
}