using Numerology.Domain.Models;
using System.Collections.Generic;

namespace Numerology.Domain.ModelExtensions
{
    public static class NameModelExtension
    {
        public static bool HasLetter(this NameModel input, IList<char> letters)
        {
            foreach(var letter in input.Name)
            {
                if (letters.Contains(letter))
                    return true;
            }

            return false;
        }
    }
}
