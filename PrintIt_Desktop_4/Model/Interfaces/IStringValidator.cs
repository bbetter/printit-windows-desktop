using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintIt_Desktop_4.Model.Interfaces
{
        public interface IStringValidator<T> where T : struct
        {
            T[] Validate(String data);
        }
}
