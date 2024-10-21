using HTTP.Requests;
using HTTP.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Controllers
{
    public class AboutController:BaseController
    {
        public IHttpResponse Index(IHttpRequest request) {
             return this.View();
        }
    }
}
