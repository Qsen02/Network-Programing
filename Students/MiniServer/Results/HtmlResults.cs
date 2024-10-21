using HTTP.Enums;
using HTTP.Headers;
using HTTP.Response;
using System.Text;
using System.Threading.Tasks;

namespace MiniServer.Results
{
    public class HtmlResults:HttpResponse
    {
        public HtmlResults
        (
            string content, 
            HttpResponseStatusCode responseStatusCode,
            string contentType = "text/html; charset=utf-8"
        ) : base(responseStatusCode)
        { 
            this.Headers.AddHeader(new HttpHeader("Content-Type", contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
