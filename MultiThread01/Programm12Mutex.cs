using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm12Mutex
    {
        static int x = 0;

        // static object locker = new();  // объект-заглушка

        // создание объекта mutex
        static Mutex mutexObj = new();

        private static void Main12(string[] args)
        {

            // запускаем пять потоков
            for (int i = 1; i < 6; i++)
            {
                Thread myThread = new(Print);
                myThread.Name = $"Поток {i}";   // устанавливаем имя для каждого потока
                myThread.Start();
            }
        }

        static void Print()
        {
            mutexObj.WaitOne();     // приостанавливаем поток до получения мьютекса
            x = 1;
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            mutexObj.ReleaseMutex();    // освобождаем мьютекс
        }

    }
}
