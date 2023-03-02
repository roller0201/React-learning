using Numerology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Numerology.Application.Interfaces
{
    public interface INumerologyCalculator
    {
        Task<string[]> CalculateNumerologyBirthDate(DateTime birthDate);
        Task<string[]> CalculateNumerologyBirthDate(string birthDate);
        Task<string[]> CalculateTreeBirthDate(DateTime birthDate);
        Task<string[]> CalculateTreeBirthDate(string year, string month, string day);

        Task<string> CalculateInsideNumber(string name, IList<LetterModel> letters);
        Task<string> CalculateOutsideNumber(string name, IList<LetterModel> letters);
        Task<string> CalculateInsideNumber(string[] names, IList<LetterModel> letters);
        Task<string> CalculateOutsideNumber(string[] names, IList<LetterModel> letters);

        Task<string> CalculateNameUpRow(string names, IList<LetterModel> letters);
        Task<string> CalculateNameMainUpRow(string nameUpRow);
        Task<string> CalculateNameDownRow(string names, IList<LetterModel> letters);
        Task<string> CalculateNameMainDownRow(string[] namesDownRow);

        Task<string> CalculateNameMainRow(string[] names);

        Task<Dictionary<string, string>> CalculateEachNumber(string names, IList<LetterModel> letters);
    }
}
