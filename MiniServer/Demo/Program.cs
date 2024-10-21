using MiniServer;
using MiniServer.Routing;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            serverRoutingTable.Add( HTTP.Enums.HttpRequestMethod.Get, "/", request => new HomeController().Index(request) );

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
