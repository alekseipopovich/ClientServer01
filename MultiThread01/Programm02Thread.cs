using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm02Thread
    {
        static int count = 1;
        private static void Main02(string[] args)
        {
            Thread newThread = new Thread(DoWork); // Создание нового потока
            newThread.Start(); // Запуск потока

            //new Thread(DoWork).Start();
        }

        static void DoWork()
        {
            // Код, который будет выполняться в новом потоке
            
            Console.WriteLine($"поток: {count++}");
        }
    }
}
