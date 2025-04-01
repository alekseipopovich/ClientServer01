using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm25AsyncTryCatch
    {
        static async Task Main25()
        {
            try
            {
                await PrintAsync("Привет всем!");
                await PrintAsync("Эй, друг?");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task PrintAsync(string message)
        {
            // если длина строки меньше 3 символов, генерируем исключение
            if (message.Length < 3)
                throw new ArgumentException($"Invalid string length: {message.Length}");
            await Task.Delay(100);     // имитация продолжительной операции
            Console.WriteLine(message);
        }
    }
}
