using HTTP.Enums;
using HTTP.Headers;

namespace HTTP.Response
{
    public interface IHttpResponse
    {
        HttpResponseStatusCode StatusCode { get; set; }
        HttpHeaderCollection Headers { get; }

        byte[] Content { get; set; }
        void AddHeader(HttpHeader header);
        byte[] GetBytes();
    }
}
