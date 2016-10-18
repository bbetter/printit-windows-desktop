using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintIt_Desktop_4.Model.Enums
{
    public enum PasswordValidationState
    {
        Valid,
        TooShort,
        TooLong,
        NotEnoughGroups,
        NoSpecial,
        NoUpperCase,
        NoDigits,
        NoLowerCase
    }

    public enum LoginValidationState
    {
        Valid,
        TooShort,
        TooLong,
        HaveRestricterdSymbols,
        NotEMail,
        Inappropriate
    }
}
