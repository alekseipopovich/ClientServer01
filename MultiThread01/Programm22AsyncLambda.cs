using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm22AsyncLambda
    {
        static async Task Main22()
        {
            // асинхронное лямбда-выражение
            Func<string, Task> printer = async (message) =>
            {
                await Task.Delay(1000);
                Console.WriteLine(message);
            };

            await printer("Привет всем!");
            await printer("Отличный день!");

            //var task1 = printer("Привет всем!");  // Запускаем без await
            //var task2 = printer("Отличный день!"); // Запускаем без await

            //await Task.WhenAll(task1, task2);      // Ожидаем обе задачи

        }
    }
}
