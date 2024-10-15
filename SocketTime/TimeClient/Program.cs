using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TimeClient
{
    public class TimeClient {
        public TimeClient(string host,int port) {
            Socket client = null;
            string request = "What day time is now";
            byte[] byteSend=Encoding.ASCII.GetBytes(request);
            byte[] bytesReceived = new byte[1024];

            try
            {
                for (int i = 0; i < 6; i++) {
                    IPAddress ipAddress = Dns.GetHostEntry(host).AddressList[0];
                    Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(new IPEndPoint(ipAddress, port));
                    Console.WriteLine("\nClient connected to server: {0}", socket.RemoteEndPoint);

                    socket.Send(byteSend, 0, byteSend.Length, SocketFlags.None);
                    Console.WriteLine("Request: {0}", request);

                    int bytes = socket.Receive(bytesReceived, 0);
                    string response = Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                    Console.WriteLine("Response: {0}", response);

                    socket.Close();
                    Thread.Sleep(2050);
                }
            }
            catch (Exception err) {
                Console.WriteLine(err.Message);
            }
            finally {
                Console.ReadKey();
                Socket socket = null;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            new TimeClient("127.0.0.1", 7066);
        }
    }
}
