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
        public static class Validation
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

        public static class Localization
        {
            public static String GetDefaultLocale()
            {
                return @"uk-UA";
            }
        }

        public static class Wrappers
        {
            public static String GetStopMessage()
            {
                return @"WRAPPER_CMD_STOP";
            }
        }

        public static class Storage
        {
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
        }

        public static class Networking
        {
            public static class API
            {
                public static class SignIn
                {
                    public static String GetLoginParamName()
                    {
                        return @"email";
                    }

                    public static String GetPasswordParamName()
                    {
                        return @"password";
                    }
                }

                public static class SignUp
                {
                    public static String GetLoginParamName()
                    {
                        return @"user[email]";
                    }

                    public static String GetPasswordParamName()
                    {
                        return @"user[password]";
                    }
                    public static String GetPasswordConfirmParamName()
                    {
                        return @"user[password_confirmation]";
                    }

                    public static String GetRoleParamName()
                    {
                        return @"user[role]";
                    }

                    public static String GetFirstNameParamName()
                    {
                        return @"user[first_name]";
                    }

                    public static String GetLastNameParamName()
                    {
                        return @"user[last_name]";
                    }

                    public static String GetPrintSpotNameParamName()
                    {
                        return @"user[print_spot_attributes][0][name]";
                    }

                    public static String GetPrintSpotAddressParamName()
                    {
                        return @"user[print_spot_attributes][0][address]";
                    }
                }

            }

            public static String GetServerAddress()
            {
                //return @"http://a682de04.ngrok.io";
                return @"http://printz.pp.ua";
            }

            public static String GetServerName()
            {
                return @"printz.pp.ua";
            }
            public static String GetWebSocketAddress()
            {
                return @"ws://printz.pp.ua/cable";
            }

            public static String GetSignIn()
            {
                return @"/api/v1/sign_in";
            }
            public static String GetSignUp()
            {
                return @"/api/v1/users";
            }
        }
    }
}
