using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Abstractions;
using PrintIt_Desktop_4.Model.DataValidation;

namespace PrintIt_Desktop_4.Model.Configuration
{
    public static class Config
    {
        public static LoginValidator GetLoginValidator()
        {
        return new EmailLoginValidator();
        }

        public static PasswordValidator GetPasswordValidator()
        {
        return new SimplePasswordValidator();
        }
    }
}
