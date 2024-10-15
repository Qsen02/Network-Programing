using HTTP.Common;
using HTTP.Enums;
using HTTP.Headers;
using System.Text;

namespace HTTP.Response
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode):this() {
            CoreValidator.ThrowIfNull(statusCode, name: nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public HttpHeaderCollection Headers { get; }

        public byte[] Content { get; set ; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            byte[] httpResponseWthoutBody = System.Text.Encoding.UTF8.GetBytes(this.ToString());
            byte[] httpResponseWithBody = new byte[httpResponseWthoutBody.Length + this.Content.Length];

            for (int i = 0; i < httpResponseWthoutBody.Length; i++) {
                httpResponseWithBody[i] = httpResponseWthoutBody[i];
            }

            for (int i = 0; i < httpResponseWithBody.Length; i++)
            {
                httpResponseWithBody[httpResponseWthoutBody.Length+1] = this.Content[i];
            }
            return httpResponseWithBody;
        }
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append($"{GloablConstrains.HttpOneProtocolFragment} {(int)StatusCode} {StatusCode.ToString()}");
            s.Append($"{GloablConstrains.HttpNewLine}");
            s.Append($"{this.Headers}");
            s.Append($"{GloablConstrains.HttpNewLine}");
            s.Append($"{GloablConstrains.HttpNewLine}");
            return s.ToString();
        }
    }
}
