using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP.Exceptions
{
    public class InternalServerErrorException:Exception
    {
        private const string error = "The server encountered an error.";

        public InternalServerErrorException():base(error) {
        //nope
        }
        public InternalServerErrorException(string message):base(message) {
        //nope
        }
    }
}
