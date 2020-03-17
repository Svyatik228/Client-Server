using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Trying
{

    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            // 1) IPv4  2)для tcp-Stream  3)Protocol
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //зв'язування з портом
            tcpSocket.Bind(tcpEndPoint);
            //режим прослуховування 
            tcpSocket.Listen(10);
            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);
                Console.WriteLine(data);
                listener.Send(Encoding.UTF8.GetBytes("Успех"));
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
