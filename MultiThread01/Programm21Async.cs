using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm21Async
    {
        static async Task Main21(string[] arg)
        {

            var task1 = CallOut();
            await task1;   // вызов асинхронного метода       
            Console.WriteLine("Некоторые действия Main");

        }

        static async Task CountAsync()
        {

            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(500); //метод Task.Delay сам по себе представляет асинхронную операцию, поэтому к нему применяется оператор await
                Console.WriteLine($"count: #{i}");
            }
        }

        // определение асинхронного метода
        async static Task CallOut()
        {
            Console.WriteLine("Начало метода CallOut"); // выполняется синхронно
            await Task.Run(CountAsync);                // выполняется асинхронно
            Console.WriteLine("Конец метода CallOut");
        }
    }
}
