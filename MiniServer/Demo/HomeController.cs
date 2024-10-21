using HTTP.Enums;
using HTTP.Requests;
using HTTP.Response;
using MiniServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest request) {
            string content = "<h1>Hello World!</h1>";
            return new HtmlResults(content, HttpResponseStatusCode.Ok);
        }

    }
}
