using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using PrintIt_Desktop_4.Model.Enums;

namespace PrintIt_Desktop_4.Model.Configuration
{
    public static class Localizer
    {
        public static string GetLanguage()
        {
            return "uk-UA";
        }

        public static string GetString(PasswordValidationState state)
        {
            string s;
            switch (state)
            {
                case PasswordValidationState.Valid:
                    s = "Пароль задовільняє вимоги";
                    break;
                case PasswordValidationState.NoDigits:
                    s = "У паролі відсутні цифри";
                    break;
                case PasswordValidationState.NoLowerCase:
                    s = "У паролі відсутні малі букви";
                    break;
                case PasswordValidationState.NoSpecial:
                    s = "У паролі відсутні спеціальні символи";
                    break;
                case PasswordValidationState.NoUpperCase:
                    s = "У паролі відсутні великі букви";
                    break;
                case PasswordValidationState.NotEnoughGroups:
                    s = "У паролі використано замало груп символів";
                    break;
                case PasswordValidationState.TooLong:
                    s = "Пароль задовгий";
                    break;
                case PasswordValidationState.TooShort:
                    s = "Пароль закороткий";
                    break;
                default:
                    s = Constants.LocalizationNotFound;
                    break;
            }
            return s;
        }

        public static string GetString(LoginValidationState state)
        {
            string s;
            switch (state)
            {
                case LoginValidationState.Valid:
                    s = "Логін задовільняє вимоги";
                    break;
                case LoginValidationState.HaveRestricterdSymbols:
                    s = "Логін містить заборонені символи";
                    break;
                case LoginValidationState.Inappropriate:
                    s = "Логін містить нецензурні вирази";
                    break;
                case LoginValidationState.NotEMail:
                    s = "Логін має бути E-mail адресою";
                    break;
                case LoginValidationState.TooLong:
                    s = "Логін задовгий";
                    break;
                case LoginValidationState.TooShort:
                    s = "Логін закороткий";
                    break;
                default:
                    s = Constants.LocalizationNotFound;
                    break;
            }
            return s;
        }
    }
}
