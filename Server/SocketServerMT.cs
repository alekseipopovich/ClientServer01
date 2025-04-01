using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class SocketServerMT
    {
        private const int Port = 11000;
        private const int Backlog = 100;
        private static int _requestCount = 0;

        static async Task Main01(string[] args)
        {
            Console.WriteLine("Многопоточный сервер запущен...");

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, Port));
            listener.Listen(Backlog);

            try
            {
                while (true)
                {
                    var clientSocket = await listener.AcceptAsync();
                    _ = HandleClientAsync(clientSocket); // Запуск обработки клиента в отдельном Task
                }
            }
            finally
            {
                listener.Close();
            }
        }

        private static async Task HandleClientAsync(Socket clientSocket)
        {
            try
            {
                int requestId = Interlocked.Increment(ref _requestCount);
                Console.WriteLine($"Обработка запроса #{requestId} в потоке {Environment.CurrentManagedThreadId}");

                var buffer = new byte[1024];
                int received = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                string request = Encoding.ASCII.GetString(buffer, 0, received);
                Console.WriteLine($"Получено: {request}");

                // Имитация обработки запроса
                await Task.Delay(100);

                string response = $"Ответ на запрос #{requestId}: {request.ToUpper()}";
                byte[] responseData = Encoding.ASCII.GetBytes(response);
                await clientSocket.SendAsync(new ArraySegment<byte>(responseData), SocketFlags.None);

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки запроса: {ex.Message}");
            }
        }
    }
}
