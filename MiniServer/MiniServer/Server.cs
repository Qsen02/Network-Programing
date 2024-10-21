using MiniServer.Routing;
using System.Net.Sockets;

namespace MiniServer
{
    public class Server
    {
        private const string LocalHostIpAddress = "127.0.0.1";
        private readonly int port;
        private readonly TcpListener listener;
        private readonly IServerRoutingTable serverRoutingTable;
        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable)
        {
            this.port = port;
            this.listener = new TcpListener(System.Net.IPAddress.Parse(LocalHostIpAddress),port);
            this.serverRoutingTable = serverRoutingTable;
        }
        public void Run() {
            this.listener.Start();
            this.isRunning = true;
            Console.WriteLine($"Server started at http://{LocalHostIpAddress}:{port}");

            while (isRunning) {
                Console.WriteLine("Waiting for client...");
                var client = this.listener.AcceptSocketAsync().GetAwaiter().GetResult();
                Task.Run(() => this.Listen(client));
            }

        }

        public async Task Listen(Socket client) {
            var connectionHandler = new ConnectionHandler(client, this.serverRoutingTable);
            await connectionHandler.ProcessRequestAsync();
        }
    }
}
