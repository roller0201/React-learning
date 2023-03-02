using Numerology.Application.Interfaces;
using Numerology.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numerology.Application.Init
{
    public static class LetterDBInit
    {
        public static IList<LetterModel> BaseLetterList { get
            {
                return new LetterModel[] { new LetterModel { Letter = 'A', Value = 1, Vowel = true},
                                                             new LetterModel { Letter = 'B', Value = 2, Vowel = false },
                                                             new LetterModel { Letter = 'C', Value = 3, Vowel = false },
                                                             new LetterModel { Letter = 'D', Value = 4, Vowel = false },
                                                             new LetterModel { Letter = 'E', Value = 5, Vowel = true },
                                                             new LetterModel { Letter = 'F', Value = 6, Vowel = false },
                                                             new LetterModel { Letter = 'G', Value = 7, Vowel = false },
                                                             new LetterModel { Letter = 'H', Value = 8, Vowel = false },
                                                             new LetterModel { Letter = 'I', Value = 9, Vowel = true },
                                                             new LetterModel { Letter = 'J', Value = 1, Vowel = false },
                                                             new LetterModel { Letter = 'K', Value = 2, Vowel = false },
                                                             new LetterModel { Letter = 'L', Value = 3, Vowel = false },
                                                             new LetterModel { Letter = 'M', Value = 4, Vowel = false },
                                                             new LetterModel { Letter = 'N', Value = 5, Vowel = false },
                                                             new LetterModel { Letter = 'O', Value = 6, Vowel = true },
                                                             new LetterModel { Letter = 'P', Value = 7, Vowel = false },
                                                             new LetterModel { Letter = 'Q', Value = 8, Vowel = false },
                                                             new LetterModel { Letter = 'R', Value = 9, Vowel = false },
                                                             new LetterModel { Letter = 'S', Value = 1, Vowel = false },
                                                             new LetterModel { Letter = 'T', Value = 2, Vowel = false },
                                                             new LetterModel { Letter = 'U', Value = 3, Vowel = true },
                                                             new LetterModel { Letter = 'V', Value = 4, Vowel = false },
                                                             new LetterModel { Letter = 'W', Value = 5, Vowel = false },
                                                             new LetterModel { Letter = 'X', Value = 6, Vowel = false },
                                                             new LetterModel { Letter = 'Y', Value = 7, Vowel = false },
                                                             new LetterModel { Letter = 'Z', Value = 8, Vowel = false },
                                                             new LetterModel { Letter = 'Ł', Value = 3, Vowel = false },
                                                             new LetterModel { Letter = 'Ń', Value = 5, Vowel = false },
                                                             new LetterModel { Letter = 'Ą', Value = 1, Vowel = false },
                                                             new LetterModel { Letter = 'Ę', Value = 5, Vowel = false },
                                                             new LetterModel { Letter = 'Ś', Value = 1, Vowel = false },
                                                             new LetterModel { Letter = 'Ć', Value = 3, Vowel = false },
                                                            };
            } }

        public async static Task InitLetters(ILetterService letterService)
        {
            LetterModel[] letterModels = BaseLetterList.ToArray();

            foreach(var letterModel in letterModels)
            {
                await letterService.AddOrUpdate(letterModel);
            }
        }
    }
}
