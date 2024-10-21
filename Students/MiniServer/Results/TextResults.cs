using HTTP.Enums;
using HTTP.Headers;
using HTTP.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiniServer.Results
{
    public class TextResults : HttpResponse
    {
        public TextResults
        (
            string content, 
            HttpResponseStatusCode responseStatusCode,
            string contentType="text/plain; charset=utf-8"
        ) : base(responseStatusCode)
        { 
            this.Headers.AddHeader(new HttpHeader("Content-type", contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResults
        (
            byte[] content, 
            HttpResponseStatusCode responseStatusCode,
            string contentType = "text/plain; charset=utf-8"
        ) : base(responseStatusCode)
        { 
            this.Headers.AddHeader(new HttpHeader("Content-type", contentType));
            this.Content = content;
        }
    }
}
