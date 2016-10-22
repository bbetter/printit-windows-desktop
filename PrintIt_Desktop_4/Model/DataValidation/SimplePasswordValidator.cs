using System.Collections.Generic;
using PrintIt_Desktop_4.Model.Abstractions;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Enums;

namespace PrintIt_Desktop_4.Model.DataValidation
{
    public class SimplePasswordValidator: PasswordValidator
    {
        public override PasswordValidationState[] Validate(string data)
        {
            var res = new List<PasswordValidationState>();
            if(data.Length>Constants.MaxPasswordLength) res.Add(PasswordValidationState.TooLong);
            if(data.Length<Constants.MinPasswordLenght) res.Add(PasswordValidationState.TooShort);
            if (res.Count > 0) return res.ToArray();
            return new []{PasswordValidationState.Valid};
        }
    }
}
