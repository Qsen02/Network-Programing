using HTTP.Enums;
using HTTP.Requests;
using HTTP.Response;

namespace MiniServer.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routes;
        public ServerRoutingTable() {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                [HttpRequestMethod.Get] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                  [HttpRequestMethod.Post] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                    [HttpRequestMethod.Delete] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                      [HttpRequestMethod.Put] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>()
            };
        }
        public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func)
        {
            this.routes[method][path] = func;
        }

        public bool Contains(HttpRequestMethod method, string path)
        {
            return this.routes.ContainsKey(method) && this.routes[method].ContainsKey(path);
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path)
        {
            return this.routes[requestMethod][path];
        }
    }
}
