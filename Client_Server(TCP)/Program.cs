using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main()
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            // 1) IPv4  2)для tcp-Stream  3)Protocol
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Enter Message");
            var message = Console.ReadLine();
            var data = Encoding.UTF8.GetBytes(message);
            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);
            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();
            do
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (tcpSocket.Available > 0);
            Console.WriteLine(answer.ToString());
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
            Console.ReadLine();
        }
    }
}
