using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using PrintIt_Desktop_4.Model.Abstractions;
using PrintIt_Desktop_4.Model.Enums;

namespace PrintIt_Desktop_4.Model.DataValidation
{
    public class EmailLoginValidator:LoginValidator
    {
        public override LoginValidationState[] Validate(string data)
        {
            var res = new List<LoginValidationState>();
            var email = new Regex(@"^[a-z0-9][-a-z0-9.!#$%&'*+-=?^_`{|}~\/]+@([-a-z0-9]+\.)+[a-z]{2,5}$");
            var valid = email.Match(data.ToLower());
            if(!valid.Success) res.Add(LoginValidationState.NotEMail);
            if (res.Count > 0) return res.ToArray();
            else return new[] {LoginValidationState.Valid};
        }
    }
}
