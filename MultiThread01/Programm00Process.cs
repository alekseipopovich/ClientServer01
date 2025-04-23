using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Part01MultiThread
{
    class Programm00Process
    {
        static void Main00()
        {

            var process = Process.GetCurrentProcess();
            Console.WriteLine($"Id: {process.Id}");
            Console.WriteLine($"Name: {process.ProcessName}");
            Console.WriteLine($"VirtualMemory: {process.VirtualMemorySize64}");
        }
    }
}
