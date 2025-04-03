using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm26AsyncWhen
    {
        static async Task Main26()
        {
            // определяем и запускаем задачи
            var task1 = PrintAsync("Hello C#");
            var task2 = PrintAsync("Hello Go");
            var task3 = PrintAsync("Hello Java");

            // ожидаем завершения всех задач
            await Task.WhenAll(task1, task2, task3);

            //await Task.WhenAny(task1, task2, task3);
        }

        static async Task PrintAsync(string message)
        {
            await Task.Delay(new Random().Next(1000, 2000));     // имитация продолжительной операции
            Console.WriteLine(message);
        }
    }
}
