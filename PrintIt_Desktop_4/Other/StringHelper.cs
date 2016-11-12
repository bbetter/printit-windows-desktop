using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PrintIt_Desktop_4.Other
{
    public static class StringHelper
    {
        public static String DeQuote(string data)
        {
            return data.Remove(0, 1).Remove(data.Length - 1, 1);
        }

        public static String Quote(string data)
        {
            return @"""" + data + @"""";
        }

        public static string DeleteSlashes(string data)
        {
           return data.Replace(@"\", String.Empty);
        }
    }
}
