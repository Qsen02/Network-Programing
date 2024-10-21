using HTTP.Enums;
using HTTP.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP.Requests
{
     public interface IHttpRequest
    {
        string Path {  get; }
        String Url {  get; }
        Dictionary<string,object> FormData { get; }
        Dictionary<string, object> QueryData { get; }
        IHttpHeaderCollection Headers { get; }
        HttpRequestMethod RequestMethod { get; }
    }
}
