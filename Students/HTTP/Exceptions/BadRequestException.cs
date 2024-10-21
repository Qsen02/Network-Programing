using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP.Exceptions
{
    public class BadRequestException:Exception
    {
        private const string error = "The request was malformed or contains \r unsupported elements.";
        public BadRequestException():base(error) {
            //nope
        }
        public BadRequestException(string message) : base(message)
        {
            //nope
        }
    }
}
