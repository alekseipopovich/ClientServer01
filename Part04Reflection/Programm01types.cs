using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Part04Reflection
{
    class Programm01types
    {
        static void Main()
        {
            Type myType1 = typeof(Person);
            printTypeOf(myType1);

            Person tom = new Person("Tom");
            Type myType2 = tom.GetType();

            Type? myType3 = Type.GetType("Person", false, true);
        }

        static void printTypeOf(Type myType)
        {
            Console.WriteLine($"Type: {myType}");                   // выводим тип
            Console.WriteLine($"Name: {myType.Name}");              // получаем краткое имя типа
            Console.WriteLine($"Full Name: {myType.FullName}");     // получаем полное имя типа
            Console.WriteLine($"Namespace: {myType.Namespace}");    // получаем пространство имен типа
            Console.WriteLine($"Is struct: {myType.IsValueType}");  // является ли тип структурой
            Console.WriteLine($"Is class: {myType.IsClass}");       // является ли тип классом

            Console.WriteLine($"Members: {myType.GetMembers()}");       // Вывод компонентов

            // вывод всех доступных элементов типа
            //foreach (MemberInfo member in myType.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            foreach (MemberInfo member in myType.GetMembers())
            {
                Console.WriteLine($"{member.DeclaringType} {member.MemberType} {member.Name}");
            }
        }
    }
}
