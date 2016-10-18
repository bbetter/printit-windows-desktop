using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Enums;
using PrintIt_Desktop_4.Model.Interfaces;

namespace PrintIt_Desktop_4.Model.Abstractions
{
    public abstract class LoginValidator:IStringValidator<LoginValidationState>
    {
        public abstract LoginValidationState[] Validate(string data);
    }
}
