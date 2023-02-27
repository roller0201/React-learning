using Numerology.API.ViewModels.Models.Client;
using Numerology.API.ViewModels.Models.Dictionary;
using Numerology.API.ViewModels.Models.Numerology;
using Numerology.API.ViewModels.RequestViewModel.Client;
using Numerology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Numerology.API.Helpers
{
    public static class Mapper
    {
        #region Dictionary
        public static NameModel ToNameModel(DictionaryViewModel model)
        {
            return new NameModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static LetterModel ToLetterModel(DictionaryViewModel model)
        {
            int value = 0;
            int.TryParse(model.Value, out value);

            return new LetterModel
            {
                Id = model.Id,
                Letter = model.Name[0],
                Value = value,
                Vowel = model.Vowel
            };
        }

        public static DictionaryViewModel ToDictionaryModel(NameModel model)
        {
            return new DictionaryViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Value = "0"
            };
        }

        public static DictionaryViewModel ToDictionaryModel(LetterModel model)
        {
            return new DictionaryViewModel
            {
                Id = model.Id,
                Name = model.Letter.ToString(),
                Value = model.Value.ToString(),
                Vowel = model.Vowel
            };
        }

        public static IList<DictionaryViewModel> ToDictionaryViewModelList(this IList<NameModel> names)
        {
            List<DictionaryViewModel> result = new List<DictionaryViewModel>();

            foreach(var name in names)
            {
                result.Add(Mapper.ToDictionaryModel(name));
            }

            return result;
        }

        public static IList<DictionaryViewModel> ToDictionaryViewModelList(this IList<LetterModel> letters)
        {
            List<DictionaryViewModel> result = new List<DictionaryViewModel>();

            foreach (var name in letters)
            {
                result.Add(Mapper.ToDictionaryModel(name));
            }

            return result;
        }

        #endregion

        #region Client

        public static ClientModel ToClientModel(AddOrUpdateClientRequest model)
        {
            return new ClientModel
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = DateTime.ParseExact(model.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Active = model.Active.GetValueOrDefault(true),
                Email = model.Email,
                Skype = model.Skype,
                Telephone = model.Telephone
            };
        }

        public static ClientViewModel ToClientViewModel(ClientModel model)
        {
            return new ClientViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate.ToString("yyyy-MM-dd"),
                Email = model.Email,
                Skype = model.Skype,
                Telephone = model.Telephone,
                EntryDate = model.EntryDate.ToString("yyyy-MM-dd")
            };
        }

        public static IList<ClientViewModel> ToClientViewModelList(IList<ClientModel> models)
        {
            IList<ClientViewModel> result = new List<ClientViewModel>();

            foreach(var model in models)
            {
                result.Add(Mapper.ToClientViewModel(model));
            }

            return result;
        }

        #endregion

        #region Client Meetings

        public static ClientMeetingsViewModel ToClientMeetingsViewModel(ClientMeetings model)
        {
            return new ClientMeetingsViewModel
            {
                Id = model.Id,
                ClientId = model.ClientId,
                EntryDate = model.EntryDate,
                MeetingDate = model.MeetingDate,
                MettingDurationHour = model.MettingDurationHour,
                MettingDurationMinute = model.MettingDurationMinute,
                MettingHour = model.MettingHour,
                MettingMinute = model.MettingMinute
            };
        }

        public static ClientMeetings ToClientMeetings(ClientMeetingsViewModel model)
        {
            return new ClientMeetings
            {
                Id = model.Id,
                ClientId = model.ClientId,
                EntryDate = model.EntryDate,
                MeetingDate = model.MeetingDate,
                MettingDurationHour = model.MettingDurationHour,
                MettingDurationMinute = model.MettingDurationMinute,
                MettingHour = model.MettingHour,
                MettingMinute = model.MettingMinute
            };
        }

        #endregion

        #region Numerology

        public static NumerologyPortraitViewModel ToNumerologyPortraitViewModel(NumerologyPortraitModel model)
        {
            return new NumerologyPortraitViewModel
            {
                Id = model.Id,
                AddedNames = model.AddedNames,
                ClientId = model.ClientId,
                Note = model.Note,
                BaseNames = model.BaseNames,
                BirthDate = model.BirthDay.ToString("yyyy-MM-dd"),
                SaveTime = model.SaveTime.ToString("yyyy-MM-dd")
            };
        }

        public static NumerologyPortraitModel ToNumerologyPortraitModel(NumerologyPortraitViewModel model)
        {
            return new NumerologyPortraitModel
            {
                Id = model.Id,
                AddedNames = model.AddedNames,
                ClientId = model.ClientId,
                Note = model.Note,
                BaseNames = model.BaseNames,
                BirthDay = DateTime.ParseExact(model.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                SaveTime = string.IsNullOrEmpty(model.SaveTime) ? DateTime.Now : DateTime.ParseExact(model.SaveTime, "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
        }

        public static IList<NumerologyPortraitViewModel> ToNumerologyPortraitViewModelList(IEnumerable<NumerologyPortraitModel> models)
        {
            IList<NumerologyPortraitViewModel> result = new List<NumerologyPortraitViewModel>();

            foreach(var model in models)
            {
                result.Add(Mapper.ToNumerologyPortraitViewModel(model));
            }

            return result;
        }

        #endregion
    }
}
