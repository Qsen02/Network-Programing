using HTTP.Enums;
using HTTP.Response;
using MiniServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Students.Controllers
{
    public class BaseController
    {
        protected Dictionary<string, object> ViewData;
        protected BaseController() { 
        this.ViewData = new Dictionary<string, object>();
        }

        private string ParseTemplate(string viewContent) {
            foreach (var param in ViewData) {
                viewContent = viewContent.Replace($"@Model.{param.Key}", $"{param.Value.ToString()}");
            }
            return viewContent;
        }
        protected IHttpResponse View([CallerMemberName] string view = null) {
            string controller = this.GetType().Name.Replace("Controller", string.Empty);
            string viewContent=System.IO.File.ReadAllText($"Views/{controller}/{view}.html");
            viewContent=this.ParseTemplate(viewContent);
            var htmlResult = new HtmlResults(viewContent, HttpResponseStatusCode.Ok);
            return htmlResult;
        }

        protected IHttpResponse Redirect(string url) {
            return new RedirectResults(url);
        }
    }
}
