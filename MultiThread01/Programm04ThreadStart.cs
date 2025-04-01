using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm04ThreadStart
    {
        //делегат представляет действие,
        //которое не принимает никаких параметров и не возвращает никакого значения
        
        //public delegate void ThreadStart();
        private static void Main04(string[] args)
        {
            // создаем новый поток
            Thread myThread1 = new Thread(Print);
            Thread myThread2 = new Thread(new ThreadStart(Print));
            Thread myThread3 = new Thread(() => Console.WriteLine("Hello Threads"));

            myThread1.Start();  // запускаем поток myThread1
            myThread2.Start();  // запускаем поток myThread2
            myThread3.Start();  // запускаем поток myThread3


            void Print() => Console.WriteLine("Hello Threads");
        }

    }
}
