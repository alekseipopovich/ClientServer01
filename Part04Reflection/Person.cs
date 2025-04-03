using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Part04Reflection
{
    public class Person
    {
        string name;
        public int Age { get; set; }

        public Person(string name) => this.name = name;
        public Person(string name, int age)
        {
            this.name = name;
            this.Age = age;
        }
        public void Print() => Console.WriteLine($"Name: {name} Age: {Age}");
    }

}
