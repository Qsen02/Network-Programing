using HTTP.Enums;
using HTTP.Headers;
using HTTP.Response;

namespace MiniServer.Results
{
    public class RedirectResults:HttpResponse
    {
        public RedirectResults(string location):base(HttpResponseStatusCode.SeeOther) 
        {
            this.Headers.AddHeader(new HttpHeader("Location", location));
        }
    }
}
