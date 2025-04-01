using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm09loker
    {
        static int x = 0;

        static object locker = new();  // объект-заглушка

        private static void Main09(string[] args)
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
            x = 1;
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
        }

        static void PrintLocker()
        {
            lock (locker)
            {
                x = 1;
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
        }
        
    }
}
