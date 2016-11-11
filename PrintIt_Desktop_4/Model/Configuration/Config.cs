using System;
using System.Collections.Generic;
using System.IO;
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
        public static String GetSignUp()
        {
            return @"/api/v1/users";    
        }

        public static String GetSignInLoginParamName()
        {
            return @"email";
        }

        public static String GetSignInPasswordParamName()
        {
            return @"password";
        }

        public static String GetSignUpLoginParamName()
        {
            return @"user[email]";
        }

        public static String GetSignUpPasswordParamName()
        {
            return @"user[password]";
        }
        public static String GetSignUpPasswordConfirmParamName()
        {
            return @"user[password_confirmation]";
        }

        public static String GetSignUpRoleParamName()
        {
            return @"user[role]";
        }

        public static String GetSignUpFirstNameParamName()
        {
            return @"user[first_name]";
        }

        public static String GetSignUpLastNameParamName()
        {
            return @"user[last_name]";
        }

        public static String GetSignUpPrintSpotNameParamName()
        {
            return @"user[print_spot_attributes][0][name]";
        }

        public static String GetSignUpPrintSpotAddressParamName()
        {
            return @"user[print_spot_attributes][0][address]";
        }

        public static String GetWebSocketAddress()
        {
            //return @"ws://localhost:8080/cable";
            return @"ws://printz.pp.ua/cable";
        }

        public static String GetStopMessage()
        {
            return @"WRAPPER_CMD_STOP";
        }

        public static String GetConfigLocation()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, @"PrintZ\config.cfg");
        }

        public static String GetDirectoryLocation()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, @"PrintZ");
        }
        public static String GetDefaultLocale()
        {
            return @"uk-UA";
        }
    }
}
