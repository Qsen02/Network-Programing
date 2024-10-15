using System.Net;
using System.Net.Sockets;

using System.Text;

namespace SocketServer
{
    public class TimeServer {
        public TimeServer(int port) {
            Socket server = null;

            try
            {
                IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(new IPEndPoint(ipAddress,port));
                server.Listen(10);

                while (true) {
                    Socket client = server.Accept();
                    Console.WriteLine("\n server connected from client: {0}", client.RemoteEndPoint);

                    byte[] bytesReceived = new byte[1024];
                    int bytes=client.Receive(bytesReceived);
                    string request = Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                    Console.WriteLine("Request: {0}",request);

                    string response = DateTime.Now.ToString();
                    byte[] byteSend = new byte[1024];
                    byteSend=Encoding.ASCII.GetBytes(response);
                    client.Send(byteSend, 0, byteSend.Length, SocketFlags.None);
                    Console.WriteLine("Response: {0}",response);
                }
            }
            catch (Exception err) {
                Console.WriteLine(err.Message);
            }
        }    
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            new TimeServer(7066);
        }
    }
}
