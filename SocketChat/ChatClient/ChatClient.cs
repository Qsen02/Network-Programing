using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatClient
{
    public class ChatClient
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress iPAddress = ipHostInfo.AddressList[0];
            IPEndPoint locaclEndPoint = new IPEndPoint(iPAddress,11000);

            Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(locaclEndPoint);
                Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                while (true)
                {
                    Console.Write(">");
                    string message = Console.ReadLine();
                    byte[] msg = Encoding.ASCII.GetBytes(message + "<EOF>");

                    int byteSent = sender.Send(msg);

                    if (message == "Exit") {
                        break;
                    }
                }
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
