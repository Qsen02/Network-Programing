using HTTP.Common;
using HTTP.Enums;
using HTTP.Exceptions;
using HTTP.Headers;

namespace HTTP.Requests
{
    public class HttpRequest:IHttpRequest
    {
        public string Path {  get; private set; }
        public string Url {  get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }
        public HttpRequest(string requestString) {
            CoreValidator.ThrowIfNullOrEmpty(text: requestString, name: nameof(requestString));
            this.FormData=new Dictionary<string, object>();
            this.QueryData=new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        private void ParseRequest(string requestString) {
            string[] splitRequestContent=requestString.Split(new[] {GloablConstrains.HttpNewLine}, StringSplitOptions.None);
            string[] requestLine = splitRequestContent[0].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine)) {
                throw new BadRequestException();
            }
            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());
            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length-1]);
        }
        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length != 3 || requestLine[2] != GloablConstrains.HttpOneProtocolFragment) return false;
            return true;
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            HttpRequestMethod requestMethod;
            bool parseResult = Enum.TryParse<HttpRequestMethod>(requestLine[0], true, out requestMethod);
            if (!parseResult) throw new BadRequestException();
            this.RequestMethod = requestMethod;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split(new[] { '?', '#' }, StringSplitOptions.None)[0];
        }

        private void ParseHeaders(string[] requestContent)
        {
            foreach (var line in requestContent)
            {
                if (string.IsNullOrWhiteSpace(line)) return;

                string[] headerParts = line.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                if (headerParts.Length != 2) throw new BadRequestException();

                HttpHeader header = new HttpHeader(headerParts[0], headerParts[1]);
                this.Headers.AddHeader(header);
            }
        }

        private void ParseRequestParameters(string bodyParameters)
        {
            this.ParseQueryParameters(this.Url);
            this.ParseFormDataParameters(bodyParameters);
        }

        private void ParseQueryParameters(string url)
        {
            if (!url.Contains('?')) return;

            string query = url.Split(new[] { '?' }, StringSplitOptions.None)[1];
            this.ParseParameters(query, this.QueryData);
        }

        private void ParseFormDataParameters(string bodyParameters)
        {
            if (this.RequestMethod == HttpRequestMethod.Get) return;

            this.ParseParameters(bodyParameters, this.FormData);
        }

        private void ParseParameters(string parameters, Dictionary<string, object> dict)
        {
            if (!parameters.Contains("=")) return;

            string[] parameterPairs = parameters.Split(new[] { '&' }, StringSplitOptions.None);
            foreach (var pair in parameterPairs)
            {
                string[] nameAndValue = pair.Split(new[] { '=' }, StringSplitOptions.None);
                if (nameAndValue.Length != 2) continue;

                string name = nameAndValue[0];
                string value = nameAndValue[1];
                dict[name] = value;
            }
        }
    }
}
