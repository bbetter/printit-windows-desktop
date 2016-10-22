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

        public static String GetServerAddress()
        {
            return @"http://printz.pp.ua";
        }

        public static String GetServerName()
        {
            return @"printz.pp.ua";
        }

        public static String GetSignIn()
        {
            return @"/api/v1/sign_in";
        }

        public static String GetSignInLoginParamName()
        {
            return @"email";
        }

        public static String GetSignInPasswordParamName()
        {
            return @"password";
        }
    }
}
