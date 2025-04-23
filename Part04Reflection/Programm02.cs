using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Part04Reflection
{
    class Programm02
    {
        static void Main02()
        {
            Person alex = new Person("Alex",18);
            Type alexType = alex.GetType();

            // получаем приватное поле name
            var name = alexType.GetField("name", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            // получаем значение поля name
            var value = name?.GetValue(alex);

            // получаем приватное поле name
            var ageProp = alexType.GetProperty("Age");

            // получаем значение поля name
            var valueAge = ageProp?.GetValue(alex);

            Console.WriteLine($"поле: {name}, значение: {value}");
            Console.WriteLine($"поле: {ageProp}, значение: {valueAge}");

        }
    }
}
