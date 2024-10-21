using HTTP.Common;
using HTTP.Enums;
using HTTP.Exceptions;
using HTTP.Requests;
using HTTP.Response;
using MiniServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiniServer.Routing
{
    public class ConnectionHandler
    {
        private readonly Socket client;
        private readonly IServerRoutingTable table;
        public ConnectionHandler(Socket client,IServerRoutingTable table) { 
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(table, nameof(table));

            this.client = client;
            this.table = table;
        }
        public async Task ProcessRequestAsync()
        {
            try
            {
                var request = await ReadRequest();
                if (request != null) {
                    Console.WriteLine($"Processing: {request.RequestMethod} {request.Path}");
                    IHttpResponse response = HandleRequest(request);
                    PrepareResponse(response);
                }
            }
            catch (BadRequestException err)
            {
                PrepareResponse(new TextResults(err.ToString(), HttpResponseStatusCode.BadRequest));
            }
            catch (Exception err) {
                PrepareResponse(new TextResults(err.ToString(), HttpResponseStatusCode.InternalServerError));
            }
            client.Shutdown(SocketShutdown.Both);
        }
        private async Task<IHttpRequest> ReadRequest() 
        {
            var result = new StringBuilder();
            var data=new ArraySegment<byte>(new byte[1024]);
            while (true) {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0) {
                    break;
                }
                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023) {
                    break;
                }
            }
            if (result.Length == 0) {
                return null;
            }
            return new HttpRequest(result.ToString());
        }

        private IHttpResponse HandleRequest(IHttpRequest request) 
        {
            if (!table.Contains(request.RequestMethod, request.Path)) 
            {
                return new TextResults($"Route with method {request.RequestMethod} and path {request.Path} not found.",
                    HTTP.Enums.HttpResponseStatusCode.NotFound);
            }
            return table.Get(request.RequestMethod, request.Path).Invoke(request);
        }

        private async void PrepareResponse(IHttpResponse response)
        {
            byte[] bytes = response.GetBytes();
            await client.SendAsync(bytes, SocketFlags.None);
        }
    }
}
