using System.Threading;

internal class Programm01CurrentThread
{
    private static void Main01(string[] args)
    {
        // получаем текущий поток
        Thread currentThread = Thread.CurrentThread;

        //получаем имя потока
        Console.WriteLine($"Имя потока: {currentThread.Name}");
        currentThread.Name = "Метод Main";
        Console.WriteLine($"Имя потока: {currentThread.Name}");

        Console.WriteLine($"Запущен ли поток: {currentThread.IsAlive}");
        Console.WriteLine($"Id потока: {currentThread.ManagedThreadId}");
        Console.WriteLine($"Приоритет потока: {currentThread.Priority}");
        Console.WriteLine($"Статус потока: {currentThread.ThreadState}");
    }
}