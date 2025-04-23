using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part01MultiThread
{
    class Programm08Deadlock
    {
        static readonly object lock1 = new object();
        static readonly object lock2 = new object();


        static void Main080()
        {
            // Поток 1
            new Thread(() =>
            {
                lock (lock1)
                {
                    Console.WriteLine("Поток 1 захватил lock1");
                    Thread.Sleep(1000); // Имитация работы

                    Console.WriteLine("Поток 1 пытается захватить lock2...");
                    lock (lock2) // Здесь поток 1 будет ждать
                    {
                        Console.WriteLine("Поток 1 захватил lock2");
                    }
                }
            }).Start();

            // Поток 2
            new Thread(() =>
            {
                lock (lock2)
                {
                    Console.WriteLine("Поток 2 захватил lock2");
                    Thread.Sleep(1000); // Имитация работы

                    Console.WriteLine("Поток 2 пытается захватить lock1...");
                    lock (lock1) // Здесь поток 2 будет ждать
                    {
                        Console.WriteLine("Поток 2 захватил lock1");
                    }
                }
            }).Start();

            Console.WriteLine("Основной поток завершен (но программа продолжает работать)");
        }
    }
}
