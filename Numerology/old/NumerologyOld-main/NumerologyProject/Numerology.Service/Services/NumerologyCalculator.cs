using Numerology.Application.Extensions;
using Numerology.Application.Interfaces;
using Numerology.Domain.Enums;
using Numerology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerology.Application.Services
{
    public class NumerologyCalculator : INumerologyCalculator
    {
        protected string[] GetNumbersAsString { get
            {
                return new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
            } }

        public Task<string> CalculateInsideNumber(string name, IList<LetterModel> letters)
        {
            Task<string> task = new Task<string>(() =>
            {
                var names = name.Split(' ');
                int sumResult = 0;
                for(int i = 0; i < names.Length; i++)
                {
                    sumResult += CalculateNumberForName(names[i], letters, CalculationType.InsideNumber);
                    var test = 1;
                }

                sumResult = CalculateValidNumerologyNumber(sumResult);

                return sumResult.ToString();
            });

            task.Start();

            return task;
        }

        private int CalculateValidNumerologyNumber(int sumResult)
        {
            int result = sumResult % 9;
            if (result == 0)
                return 9;
            return result;
        }

        private int CalculateNumberForName(string name, IList<LetterModel> letters, CalculationType type)
        {
            name = name.ToUpper();
            if(type == CalculationType.InsideNumber)
            {
                int sum = 0;
                for(int i = 0; i < name.Length; i++)
                {
                    var letter = letters.FirstOrDefault(x => x.Letter == name[i] && x.Vowel);
                    if(letter != null)
                    {
                        if (letter.Value == 9)
                            continue;
                        sum += letter.Value;
                    }
                }

                return sum;
            }
            else
            {
                int sum = 0;
                for (int i = 0; i < name.Length; i++)
                {
                    var letter = letters.FirstOrDefault(x => x.Letter == name[i] && !x.Vowel);
                    if (letter != null)
                    {
                        if (letter.Value == 9)
                            continue;
                        sum += letter.Value;
                    }
                }

                return sum;
            }
        }

        public Task<string> CalculateInsideNumber(string[] names, IList<LetterModel> letters)
        {
            Task<string> task = new Task<string>(() =>
            {
                int sumResult = 0;
                for (int i = 0; i < names.Length; i++)
                {
                    sumResult += CalculateNumberForName(names[i], letters, CalculationType.InsideNumber);
                }

                sumResult = CalculateValidNumerologyNumber(sumResult);

                return sumResult.ToString();
            });

            task.Start();

            return task;
        }

        public Task<string[]> CalculateNumerologyBirthDate(DateTime birthDate)
        {
            Task<string[]> task = new Task<string[]>(() =>
            {
                var birthDayString = birthDate.ToString("yyyyMMdd");
                int subNumber = CalculateNumberForDateString(birthDayString);
                int mainNumber = CalculateNumberForDateString(subNumber.ToString());

                return new string[] { mainNumber.ToString(), subNumber.ToString() };
            });

            task.Start();

            return task;
        }

        private int CalculateNumberForDateString(string numberString)
        {
            int sumResult = 0;

            for (int i = 0; i < numberString.Length; i++)
            {
                int number = int.Parse(numberString[i].ToString());
                sumResult += number;
            }

            return sumResult;
        }

        public Task<string[]> CalculateNumerologyBirthDate(string birthDate)
        {
            Task<string[]> task = new Task<string[]>(() =>
            {
                int subNumber = CalculateNumberForDateString(birthDate);
                int mainNumber = CalculateNumberForDateString(subNumber.ToString());

                while (mainNumber > 9)
                    mainNumber -= 9;

                if (subNumber == 29 || subNumber == 38)
                    return new string[] { mainNumber.ToString(), subNumber.ToString(), "11" };

                return new string[] { mainNumber.ToString(), subNumber.ToString() };
            });

            task.Start();

            return task;
        }

        public Task<string> CalculateOutsideNumber(string name, IList<LetterModel> letters)
        {
            Task<string> task = new Task<string>(() =>
            {
                var names = name.Split(' ');
                int sumResult = 0;
                for (int i = 0; i < names.Length; i++)
                {
                    sumResult += CalculateNumberForName(names[i], letters, CalculationType.OutsideNumber);
                }

                sumResult = CalculateValidNumerologyNumber(sumResult);

                return sumResult.ToString();
            });

            task.Start();

            return task;
        }

        public Task<string> CalculateOutsideNumber(string[] names, IList<LetterModel> letters)
        {
            Task<string> task = new Task<string>(() =>
            {
                int sumResult = 0;
                for (int i = 0; i < names.Length; i++)
                {
                    sumResult += CalculateNumberForName(names[i], letters, CalculationType.OutsideNumber);
                }

                sumResult = CalculateValidNumerologyNumber(sumResult);

                return sumResult.ToString();
            });

            task.Start();

            return task;
        }

        public Task<string[]> CalculateTreeBirthDate(DateTime birthDate)
        {
            Task<string[]> task = new Task<string[]>(() =>
            {
                return new string[] { "" };
            });

            task.Start();

            return task;
        }

        public Task<string[]> CalculateTreeBirthDate(string year, string month, string day)
        {
            Task<string[]> task = new Task<string[]>(() =>
            {
                int yearNumber;
                int monthNumber;
                int dayNumber;

                int.TryParse(year, out yearNumber);
                int.TryParse(month, out monthNumber);
                int.TryParse(day, out dayNumber);

                if (monthNumber > 9)
                    monthNumber -= 9;

                while (dayNumber > 9)
                    dayNumber -= 9;

                int sumYear = 0;

                for (int i = 0; i < 4; i++)
                {
                    int parsed;
                    int.TryParse(year[i].ToString(), out parsed);
                    if (parsed == 9 || parsed == 0)
                        continue;
                    sumYear += parsed;
                }

                while (sumYear > 9)
                    sumYear -= 9;

                int firstUpRowFirst = monthNumber + dayNumber;

                while (firstUpRowFirst > 9)
                    firstUpRowFirst -= 9;

                int firstUpRowSecond = dayNumber + sumYear;

                while (firstUpRowSecond > 9)
                    firstUpRowSecond -= 9;

                int secondUpRow = firstUpRowFirst + firstUpRowSecond;
                while (secondUpRow > 9)
                    secondUpRow -= 9;

                int thirdUpRow = monthNumber + sumYear;
                while (thirdUpRow > 9)
                    thirdUpRow -= 9;

                int firstDownRowFirst = Math.Abs(monthNumber - dayNumber);

                while (firstDownRowFirst > 9)
                    firstDownRowFirst -= 9;

                int firstDownRowSecond = Math.Abs(dayNumber - sumYear);

                while (firstDownRowSecond > 9)
                    firstDownRowSecond -= 9;

                int secondDownRow = Math.Abs(firstDownRowFirst - firstDownRowSecond);
                while (secondDownRow > 9)
                    secondDownRow -= 9;

                int thirdDownRow = Math.Abs(monthNumber - sumYear);
                while (thirdDownRow > 9)
                    thirdDownRow -= 9;

                IList<string> result = new List<string>();
                result.Add(firstUpRowFirst.ToString());
                result.Add(firstUpRowSecond.ToString());
                result.Add(secondUpRow.ToString());
                result.Add(thirdUpRow.ToString());
                result.Add(firstDownRowFirst.ToString());
                result.Add(firstDownRowSecond.ToString());
                result.Add(secondDownRow.ToString());
                result.Add(thirdDownRow.ToString());

                return result.ToArray();
            });

            task.Start();

            return task;
        }

        public Task<string> CalculateNameUpRow(string names, IList<LetterModel> letters)
        {
            Task<string> task = new Task<string>(() =>
            {
                var lettersToUse = letters.Where(x => x.Vowel).ToList();

                StringBuilder stringBuilder = new StringBuilder();

                var namesToProccess = names.Split(' ');

                namesToProccess = namesToProccess.Where(x => x != "-" && x != "").ToArray();

                foreach (var name in namesToProccess)
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        var letter = lettersToUse.FirstOrDefault(x => x.Letter.ToString().ToUpper() == name[i].ToString().ToUpper());
                        if (letter != null)
                        {
                            stringBuilder.Append(letter.Value.ToString());
                        }
                        else
                            stringBuilder.Append(" ");
                    }

                    stringBuilder.Append("-");
                }

                return stringBuilder.ToString();
            });

            task.Start();

            return task;
        }

        public Task<string> CalculateNameMainUpRow(string namesUpRow)
        {
            Task<string> task = new Task<string>(() =>
            {

                StringBuilder stringBuilder = new StringBuilder();
                var numbersAsString = GetNumbersAsString.ToList();

                //foreach (var name in namesUpRow)
                //{
                //    bool alreadySet = false;
                //    int number = 0;
                //    for (int i = 0; i < name.Length; i++)
                //    {
                //        if (name.Contains("9") && name.Any(x => numbersAsString.Any(y => y.Contains(x.ToString()))) != null && !alreadySet)
                //        {
                //            int outNumber;
                //            int.TryParse(name[i].ToString(), out outNumber);
                //            number += outNumber;
                //        }
                //        else if (name.Contains("9"))
                //        {
                //            alreadySet = true;
                //            int index = name.Length / 2;
                //            for (int j = 0; j < index; j++)
                //                stringBuilder.Append(" ");
                //            stringBuilder.Append("9");
                //            for (int j = index; j < name.Length; j++)
                //                stringBuilder.Append(" ");
                //        }
                //    }

                //    if (!alreadySet)
                //    {
                //        int index = name.Length / 2;
                //        for (int j = 0; j < index; j++)
                //            stringBuilder.Append(" ");
                //        stringBuilder.Append(CalculateNumberNumerology(number).ToString());
                //        for (int j = index; j < name.Length; j++)
                //            stringBuilder.Append(" ");
                //    }

                //    stringBuilder.Append("-");
                //}

                return stringBuilder.ToString();
            });

            task.Start();

            return task;
        }

        public Task<string> CalculateNameDownRow(string names, IList<LetterModel> letters)
        {
            Task<string> task = new Task<string>(() =>
            {
                var lettersToUse = letters.Where(x => x.Vowel == false).ToList();

                StringBuilder stringBuilder = new StringBuilder();

                var namesToProccess = names.Split(' ');

                namesToProccess = namesToProccess.Where(x => x != "-" && x != "").ToArray();

                foreach (var name in namesToProccess)
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        var letter = lettersToUse.FirstOrDefault(x => x.Letter.ToString().ToUpper() == name[i].ToString().ToUpper());
                        if (letter != null)
                        {
                            stringBuilder.Append(letter.Value.ToString());
                        }
                        else
                            stringBuilder.Append(" ");
                    }

                    stringBuilder.Append("-");
                }

                return stringBuilder.ToString();
            });

            task.Start();

            return task;
        }

        private int CalculateNumberNumerology(int number)
        {
            while (number > 9)
                number -= 9;

            return number;
        }

        public Task<string> CalculateNameMainDownRow(string[] namesDownRow)
        {
            Task<string> task = new Task<string>(() =>
            {

                StringBuilder stringBuilder = new StringBuilder();
                var numbersAsString = GetNumbersAsString.ToList();
                
                foreach(var name in namesDownRow)
                {
                    bool alreadySet = false;
                    int number = 0;
                    for(int i = 0; i < name.Length; i++)
                    {
                        if (name.Contains("9") && name.Any(x => numbersAsString.Any(y => y.Contains(x.ToString()))) != null && !alreadySet)
                        {
                            int outNumber;
                            int.TryParse(name[i].ToString(), out outNumber);
                            number += outNumber;
                        }
                        else if(name.Contains("9"))
                        {
                            alreadySet = true;
                            int index = name.Length / 2;
                            for (int j = 0; j < index; j++)
                                stringBuilder.Append(" ");
                            stringBuilder.Append("9");
                            for (int j = index; j < name.Length; j++)
                                stringBuilder.Append(" ");
                        }
                    }

                    if(!alreadySet)
                    {
                        int index = name.Length / 2;
                        for (int j = 0; j < index; j++)
                            stringBuilder.Append(" ");
                        stringBuilder.Append(CalculateNumberNumerology(number).ToString());
                        for (int j = index; j < name.Length; j++)
                            stringBuilder.Append(" ");
                    }

                    stringBuilder.Append("-");
                }

                return stringBuilder.ToString();
            });

            task.Start();

            return task;
        }

        public Task<string> CalculateNameMainRow(string[] names)
        {
            Task<string> task = new Task<string>(() =>
            {

                StringBuilder stringBuilder = new StringBuilder();
                var numbersAsString = GetNumbersAsString.ToList();

                names = names.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                foreach (var name in names)
                {
                    bool alreadySet = false;
                    int number = 0;
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (name.Contains("9") && name.Any(x => numbersAsString.Any(y => y.Contains(x.ToString()))) != null && !alreadySet)
                        {
                            int outNumber;
                            int.TryParse(name[i].ToString(), out outNumber);
                            number += outNumber;
                        }
                        else if(name.Any(x => numbersAsString.Any(y => y.Contains(x.ToString()))) != null && !alreadySet)
                        {
                            int outNumber;
                            int.TryParse(name[i].ToString(), out outNumber);
                            number += outNumber;
                        }
                        else if (name.Contains("9"))
                        {
                            alreadySet = true;
                            int index = name.Length / 2;
                            for (int j = 0; j < index; j++)
                                stringBuilder.Append(" ");
                            stringBuilder.Append("9");
                            for (int j = index; j < name.Length; j++)
                                stringBuilder.Append(" ");
                        }
                    }

                    if (!alreadySet)
                    {
                        int index = name.Length / 2;
                        for (int j = 0; j < index; j++)
                            stringBuilder.Append(" ");
                        stringBuilder.Append(CalculateNumberNumerology(number).ToString());
                        for (int j = index; j < name.Length; j++)
                            stringBuilder.Append(" ");
                    }

                    if(names.LastOrDefault(x => x == name) == null)
                        stringBuilder.Append("-");
                }

                return stringBuilder.ToString();
            });

            task.Start();

            return task;
        }

        public Task<Dictionary<string, string>> CalculateEachNumber(string names, IList<LetterModel> letters)
        {
            Task<Dictionary<string, string>> task = new Task<Dictionary<string, string>>(() =>
            {
                Dictionary<string, string> values = new Dictionary<string, string>();

                var numbers = GetNumbersAsString.ToList();
                numbers.Add("9");

                StringBuilder namesNumbers = new StringBuilder();

                for(int i = 0; i < names.Length; i++)
                {
                    var letter = letters.FirstOrDefault(x => x.Letter.ToString().ToUpper() == names[i].ToString().ToUpper());
                    if (letter != null)
                    {
                        namesNumbers.Append(letter.Value.ToString());
                    }
                    else
                        namesNumbers.Append(" ");
                }

                var namesToProcess = namesNumbers.ToString();

                foreach (var number in numbers)
                {
                    var indexes = namesToProcess.AllIndexesOf(number);
                    values.Add(number, indexes.Count.ToString());
                }

                return values;
            });

            task.Start();

            return task;
        }
    }
}
