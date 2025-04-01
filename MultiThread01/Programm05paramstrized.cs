using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm05paramstrized
    {
        // если нам надо передать какие-нибудь параметры в поток,
        // используется делегат ParameterizedThreadStart,
        // который передается в конструктор класса Thread

        // public delegate void ParameterizedThreadStart(object? obj);

        private static void Main05(string[] args)
        {
            // создаем новые потоки
            Thread myThread1 = new Thread(new ParameterizedThreadStart(Print));
            Thread myThread2 = new Thread(Print);
            Thread myThread3 = new Thread(message => Console.WriteLine(message));

            // запускаем потоки
            myThread1.Start("Hello");
            myThread2.Start("Привет");
            myThread3.Start("Salut");


            void Print(object? message) => Console.WriteLine(message);
        }
    }
}
