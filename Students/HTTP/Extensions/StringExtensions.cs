using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP.Extensions
{
    public class StringExtensions
    {
        public string Capitalize(string str) {
            if (string.IsNullOrEmpty(str)) {
            return str;
            }
            return char.ToUpper(str[0])+str.Substring(1);
        }
    }
}
