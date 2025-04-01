using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm07params
    {
        private static void Main07(string[] args)
        {
            Person user1 = new Person("Вася", 17);
            // создаем новый поток
            Thread myThread = new Thread(Print);
            myThread.Start(user1);

            void Print(object? obj)
            {
                // здесь мы ожидаем получить объект Person
                if (obj is Person person)
                {
                    Console.WriteLine($"Name = {person.Name}");
                    Console.WriteLine($"Age = {person.Age}");
                }
            }
        }
    }
    // определяем специальный класс Person,
    // объект которого будет передаваться во второй поток,
    // а в методе Main передаем его во второй поток.
    record class Person(string Name, int Age);
}
