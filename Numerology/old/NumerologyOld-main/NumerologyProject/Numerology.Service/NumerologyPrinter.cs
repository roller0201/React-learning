using Numerology.API.ViewModels.Models.Numerology;
using Numerology.Application.Interfaces;
using Numerology.Application.Services;
using Numerology.Domain.Models;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Numerology.Application
{
    public static class NumerologyPrinter
    {
        public static async Task<string> CreateDocument(NumerologyNameViewModel baseModel, NumerologyNameViewModel addedModel, NumerologyNameViewModel wholeModel, NumerologyPortraitModel portrait)
        {
            var config = new PdfGenerateConfig();
            config.PageOrientation = PdfSharp.PageOrientation.Landscape;
            config.PageSize = PdfSharp.PageSize.A4;

            var html = await GenerateHTML(baseModel, addedModel, wholeModel, portrait);

            PdfDocument pdf = PdfGenerator.GeneratePdf(html, config);

            var fileName = portrait.BaseNames.Replace(" ", "") + DateTime.Now.ToShortDateString().Replace("-", "").Replace("/", "");
            var path = Directory.GetCurrentDirectory() + "\\pdfs\\" + fileName + ".pdf";

            pdf.Save(path);

            return fileName;
        }

        private static async Task<string> GenerateHTML(NumerologyNameViewModel baseModel, NumerologyNameViewModel addedModel, NumerologyNameViewModel wholeModel, NumerologyPortraitModel portrait)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (addedModel == null)
                addedModel = new NumerologyNameViewModel();

			stringBuilder = stringBuilder.Append(StartHTML);

            var calculator = new NumerologyCalculator();
            var dateString = portrait.BirthDay.ToString("yyyyMMdd");
            var test = await calculator.CalculateNumerologyBirthDate(dateString);
            string mainNumber = string.Empty;

            if (test.Length > 2)
                mainNumber = test[0] + "/" + test[1] + "/" + test[2];
            else
                mainNumber = test[0] + "/" + test[1];

            stringBuilder = stringBuilder.Append(GetRowForNames(baseModel.UpRowMain));
            stringBuilder = stringBuilder.Replace("##dataUrodzenia", portrait.BirthDay.ToString("yyyy-MM-dd"));
            int month;
            int day;
            var dateSplit = portrait.BirthDay.ToString("yyyy-MM-dd").Split("-");
            int.TryParse(dateSplit[1], out month);
            int.TryParse(dateSplit[2], out day);
            int nast = month + day;
            while (nast > 9)
                nast -= 9;
            stringBuilder = stringBuilder.Replace("##liczbaNastawienia", nast.ToString());
            stringBuilder = stringBuilder.Replace("##glownaLiczba", mainNumber);

            stringBuilder = stringBuilder.Append(GetRowForNames(baseModel.UpRow));
            stringBuilder = stringBuilder.Append(GetRowForNames(portrait.BaseNames.Replace(" ", "-")));
            stringBuilder = stringBuilder.Append(GetRowForNames(baseModel.DownRow));
            stringBuilder = stringBuilder.Append(GetRowForNames(baseModel.DownRowMain));

            stringBuilder = stringBuilder.Append(StartHTML2);

            stringBuilder = stringBuilder.Append(NewNames);

            stringBuilder = stringBuilder.Append(GetRowForNames(addedModel.UpRowMain));
            stringBuilder = stringBuilder.Append(GetRowForNames(addedModel.UpRow));
            stringBuilder = stringBuilder.Append(GetRowForNames(portrait.AddedNames.Replace(" ", "-")));
            stringBuilder = stringBuilder.Append(GetRowForNames(addedModel.DownRow));
            stringBuilder = stringBuilder.Append(GetRowForNames(addedModel.DownRowMain));
            stringBuilder = stringBuilder.Append(EndNames);

            stringBuilder = stringBuilder.Append(MainNumbers);

            stringBuilder = stringBuilder.Append(GetMainNumbersString(baseModel.UpRowMainWhole, addedModel.UpRowMainWhole, wholeModel.UpRowMainWhole));
            stringBuilder = stringBuilder.Append(GetMainNumbersString(baseModel.DownRowMainWhole, addedModel.DownRowMainWhole, wholeModel.DownRowMainWhole));

            int number1;
            int number2;
            int.TryParse(baseModel.UpRowMainWhole, out number1);
            int.TryParse(baseModel.DownRowMainWhole, out number2);
            int res = number1 + number2;

            while (res > 9)
                res -= 9;

            int.TryParse(addedModel.UpRowMainWhole, out number1);
            int.TryParse(addedModel.DownRowMainWhole, out number2);
            int res2 = number1 + number2;

            while (res2 > 9)
                res2 -= 9;

            int.TryParse(wholeModel.UpRowMainWhole, out number1);
            int.TryParse(wholeModel.DownRowMainWhole, out number2);
            int res3 = number1 + number2;

            while (res3 > 9)
                res3 -= 9;

            stringBuilder = stringBuilder.Append(GetMainNumbersString2(res.ToString(), res2.ToString(), res3.ToString()));

            stringBuilder = stringBuilder.Append(EndMainNumbers);

            stringBuilder = stringBuilder.Append(GetPotentialsString(1,baseModel.One, addedModel.One, wholeModel.One));
            stringBuilder = stringBuilder.Append(GetPotentialsString(2,baseModel.Two, addedModel.Two, wholeModel.Two));
            stringBuilder = stringBuilder.Append(GetPotentialsString(3,baseModel.Three, addedModel.Three, wholeModel.Three));
            stringBuilder = stringBuilder.Append(GetPotentialsString(4,baseModel.Four, addedModel.Four, wholeModel.Four));
            stringBuilder = stringBuilder.Append(GetPotentialsString(5,baseModel.Five, addedModel.Five, wholeModel.Five));
            stringBuilder = stringBuilder.Append(GetPotentialsString(6,baseModel.Six, addedModel.Six, wholeModel.Six));
            stringBuilder = stringBuilder.Append(GetPotentialsString(7,baseModel.Seven, addedModel.Seven, wholeModel.Seven));
            stringBuilder = stringBuilder.Append(GetPotentialsString(8,baseModel.Eight, addedModel.Eight, wholeModel.Eight));
            stringBuilder = stringBuilder.Append(GetPotentialsString(9, baseModel.Nine, addedModel.Nine, wholeModel.Nine));
            stringBuilder = stringBuilder.Append($"<tr><td></td><td></td><td>{SumNumbers(baseModel)}</td><td>+</td><td>{SumNumbers(addedModel)}</td><td>=</td><td>{SumNumbers(wholeModel)}</td></tr>");


            stringBuilder = stringBuilder.Append("</tbody></table></td><td style='vertical-align: top; width: 250px'><table><tbody>");

            INumerologyCalculator numerologyCalculator = new NumerologyCalculator();
            var response = numerologyCalculator.CalculateNumerologyBirthDate(dateString).GetAwaiter().GetResult();

            int mainNumber2;
            int.TryParse(response[0], out mainNumber2);
            string date = portrait.BirthDay.ToString("yyyy-MM-dd");
            var splitedDate = date.Split("-");
            var resp = numerologyCalculator.CalculateTreeBirthDate(splitedDate[0], splitedDate[1], splitedDate[2]).GetAwaiter().GetResult();

            stringBuilder = stringBuilder.Append($"<tr><td></td><td>{(36 - mainNumber2)}</td><td>{36 - mainNumber2 + 9}</td><td>{36 - mainNumber2 + 18}</td><td>{36 - mainNumber2 + 27}</td></tr>");
            stringBuilder = stringBuilder.Append($"<tr><td>Wyzwania</td><td>{(resp[0])}</td><td>{(resp[1])}</td><td>{(resp[2])}</td><td>{(resp[3])}</td></tr>");
            stringBuilder = stringBuilder.Append($"<tr><td>Punkty zwrotne</td><td>{(resp[4])}</td><td>{(resp[5])}</td><td>{(resp[6])}</td><td>{(resp[7])}</td></tr></tbody></table></td></tr></table>");



            stringBuilder = stringBuilder.Append(@"</div>
<div style='padding-left: 30px; padding-top: 50px; margin-left: 15px;'>
Podpis
<hr/>
<hr/>
<hr/>
</div>

" + DateTime.Now.ToShortDateString() + @"
</div>
</body>
</html>");


            return stringBuilder.ToString();
        }

        private static int SumNumbers(NumerologyNameViewModel toSumPotentials)
        {
            int one;
            int two;
            int three;
            int four;
            int five;
            int six;
            int seven;
            int eight;
            int nine;

            int.TryParse(toSumPotentials.One, out one);
            int.TryParse(toSumPotentials.Two, out two);
            int.TryParse(toSumPotentials.Three, out three);
            int.TryParse(toSumPotentials.Four, out four);
            int.TryParse(toSumPotentials.Five, out five);
            int.TryParse(toSumPotentials.Six, out six);
            int.TryParse(toSumPotentials.Seven, out seven);
            int.TryParse(toSumPotentials.Eight, out eight);
            int.TryParse(toSumPotentials.Nine, out nine);

            return one + two + three + four + five + six + seven + eight + nine;
        }

        private static string GetRowForNames(string toInjectString)
        {
            StringBuilder stringBuild = new StringBuilder();

            stringBuild = stringBuild.Append("<tr>");

            for(int i = 0; i < toInjectString.Length; i++)
            {
                if(toInjectString[i].ToString() == "-")
                {
                    //This is space between names
                    stringBuild = stringBuild.Append("<td style='color: white;'>__</td>");
                }
                else
                {
                    stringBuild = stringBuild.Append($"<td>{toInjectString[i].ToString().ToUpper()}</td>");
                }
            }

            stringBuild = stringBuild.Append("</tr>");
            return stringBuild.ToString();
        }

        private static string GetMainNumbersString(string baseNumber, string addedNumber, string wholeNumber)
        {
            StringBuilder stringBuild = new StringBuilder();

            stringBuild = stringBuild.Append("<tr>");
            stringBuild = stringBuild.Append($"<td>{baseNumber}</td>");
            stringBuild = stringBuild.Append("<td>+</td>");
            stringBuild = stringBuild.Append($"<td>{addedNumber}</td>");
            stringBuild = stringBuild.Append("<td>=</td>");
            stringBuild = stringBuild.Append($"<td>{wholeNumber}</td>");
            stringBuild = stringBuild.Append("</tr>");

            return stringBuild.ToString();
        }

        private static string GetMainNumbersString2(string baseNumber, string addedNumber, string wholeNumber)
        {
            StringBuilder stringBuild = new StringBuilder();

            stringBuild = stringBuild.Append("<tr>");
            stringBuild = stringBuild.Append($"<td>{baseNumber}</td>");
            stringBuild = stringBuild.Append("<td></td>");
            stringBuild = stringBuild.Append($"<td>{addedNumber}</td>");
            stringBuild = stringBuild.Append("<td></td>");
            stringBuild = stringBuild.Append($"<td>{wholeNumber}</td>");
            stringBuild = stringBuild.Append("</tr>");

            return stringBuild.ToString();
        }

        private static string GetPotentialsString(int number, string baseNumber, string addedNumber, string wholeNumber)
        {
            StringBuilder builder = new StringBuilder();

            builder = builder.Append("<tr>");
            builder = builder.Append($"<td>{number}</td>");
            builder = builder.Append($"<td>-</td>");
            builder = builder.Append($"<td>{baseNumber}</td>");
            builder = builder.Append($"<td>+</td>");
            builder = builder.Append($"<td>{addedNumber}</td>");
            builder = builder.Append($"<td>=</td>");
            builder = builder.Append($"<td>{wholeNumber}</td>");
            builder = builder.Append("</tr>");

            return builder.ToString();
        }

        private static string EndHTML { get { return "</body></html>"; } }

        //Tutaj musimy wstrzyknac wiersze
        private static string StartHTML { get
            {
                return @"<html>
<head>  
        <style type='text/css'>  
        #st-box { 
            float:left; 

            background-color:white; 
        } 
        #nd-box { 
            float:left; 
            background-color:white;  
            margin-left:25px; 
        } 
        #rd-box { 
			margin-left:25px; 
            float:left; 
            background-color:white; 
        } 
hr {
    display: block;
    height: 1px;
    border: 0;
    border-top: 1px solid #ccc;
    margin: 1em 0;
    padding: 0;
}
        </style>  
    </head>  
<body style='width: 100%;'>
<div style='font-weight: bold; text-transform: uppercase; width: 100%;'>
<div>
<table style='width: 100%;'>
	<tr>
		<td style='width: 33%;'>
			Data urodzenia: ##dataUrodzenia
		</td>
		<td style='width: 33%;'>
			L.Nastawienia: ##liczbaNastawienia
		</td style='width: 33%;'>
		<td>
			Facebook: Obliczanie Szczęścia
		</td>
	</tr>
	<tr>
		<td>
            ##glownaLiczba
		</td>
		<td>
		</td>
		<td>
		tel. 668-479-886
		</td>
	</tr>
</table>
</div>
<div style='font-weight: bold; text-transform: uppercase; padding-top: 30px;'>
<table style='width: 100%;'>
<tbody>
<tr>
	<td style='width: 12%;'>Imiona i nazwiska:</td>
	<td style='padding-left: 12px; width: 88%;'>
	<table>
		<tbody>
		<!-- Tutaj gorny glowny wiersz 8 2 6 6-->";
            } }
		private static string StartHTML2 { get
            {
                return @"</tbody>
	</table>
	</td>
</tr>
</tbody>
</table>";
            }
        }

        private static string NewNames { get
            {
                return @"<table stlye='width: 100%;'>
<tr>
	<td style='width: 12%;'>Nowe imiona:</td>
	<td style='padding-left: 12px; width: 88%;'>
	<table>
		<tbody>";
            }
        }

        private static string EndNames
        {
            get
            {
                return @"</tbody>
	</table>
	</td>
</tr>
</table>
</div>
<div>
<div>";
            }
        }

        private static string MainNumbers
        {
            get
            {
                return @"<table>
<tr>
	<td style='vertical-align: top; width: 90px;'>
	<table >
	<tbody>";
            }
        }

        private static string EndMainNumbers
        {
            get
            {
                return @"</tbody>
</table>
	</td>
	<td style='width: 120px;'>
	<table>
	<tbody>";
            }
        }
    }
}
