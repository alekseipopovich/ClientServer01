using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm06params
    {
        private static void Main06(string[] args)
        {
            int number = 4;
            // создаем новый поток
            Thread myThread = new Thread(Print);
            myThread.Start(number);    // n * n = 16


            // действия, выполняемые во втором потокке
            void Print(object? obj)
            {
                // здесь мы ожидаем получить число
                if (obj is int n)
                {
                    Console.WriteLine($"n * n = {n * n}");
                }
            }
        }
    }
}
