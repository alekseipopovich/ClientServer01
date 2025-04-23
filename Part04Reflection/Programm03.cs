using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Part04Reflection
{
    class Programm03
    {
        static void Main03()
        {

            Type myType = typeof(Person);

            Console.WriteLine("Поля:");

            foreach (FieldInfo field in myType.GetFields(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                string modificator = "";

                // получаем модификатор доступа
                if (field.IsPublic)
                    modificator += "public ";
                else if (field.IsPrivate)
                    modificator += "private ";
                else if (field.IsAssembly)
                    modificator += "internal ";
                else if (field.IsFamily)
                    modificator += "protected ";
                else if (field.IsFamilyAndAssembly)
                    modificator += "private protected ";
                else if (field.IsFamilyOrAssembly)
                    modificator += "protected internal ";

                // если поле статическое
                if (field.IsStatic) modificator += "static ";

                Console.WriteLine($"{modificator}{field.FieldType.Name} {field.Name}");

                
            }

            foreach (PropertyInfo prop in myType.GetProperties(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                Console.Write($"{prop.PropertyType} {prop.Name} {{");

                // если свойство доступно для чтения
                if (prop.CanRead) Console.Write("get;");
                // если свойство доступно для записи
                if (prop.CanWrite) Console.Write("set;");
                Console.WriteLine("}");
            }
        }
    }
}
