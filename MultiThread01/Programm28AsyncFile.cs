using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm28AsyncFile
    {
        static async Task Main28(string[] args)
        {
            string filePath = "test.txt";

            await WriteToFileAsync(filePath, "Привет, асинхронный мир!");

            string content = await ReadFromFileAsync(filePath);
            Console.WriteLine($"Содержимое файла: {content}");
        }

        static async Task WriteToFileAsync(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }

        static async Task<string> ReadFromFileAsync(string path)
        {
            return await File.ReadAllTextAsync(path);
        }
    }
}
