using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class SocketClientMT
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 11000;
        private const int ClientCount = 20;

        static async Task Main00(string[] args)
        {
            Console.WriteLine("Client start...");

            var tasks = new Task[ClientCount];
            for (int i = 0; i < ClientCount; i++)
            {
                int clientId = i + 1;
                tasks[i] = Task.Run(() => SendRequestAsync(clientId));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("All Requests Send");
        }

        private static async Task SendRequestAsync(int clientId)
        {
            try
            {
                using var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await clientSocket.ConnectAsync(new IPEndPoint(IPAddress.Parse(ServerIp), Port));

                string message = $"Client request #{clientId}";
                byte[] requestData = Encoding.ASCII.GetBytes(message);
                await clientSocket.SendAsync(new ArraySegment<byte>(requestData), SocketFlags.None);

                var buffer = new byte[1024];
                int received = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                string response = Encoding.ASCII.GetString(buffer, 0, received);

                Console.WriteLine($"Client #{clientId} get: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client #{clientId} err: {ex.Message}");
            }
        }
    }
}
