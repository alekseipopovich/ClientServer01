namespace MultiThread01
{
    class Programm141Tasks
    {
        static void Main141()
        {
            Console.WriteLine("Начало программы");
            
            Task task1 = new Task(() => Console.WriteLine("Task1 is executed"));
            
            //Task task1 = new Task(() =>
            //{
            //    Console.WriteLine("Task 1 Starts");
            //    Thread.Sleep(1000);     // задержка на 1 секунду - имитация долгой работы
            //    Console.WriteLine("Task 1 Ends");
            //});

            task1.Start();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Task2 is executed"));

            Task task3 = Task.Run(() => Console.WriteLine("Task3 is executed"));

            //Task.WaitAll(task2, task1, task3);

            task1.Wait();   // ожидаем завершения задачи task1
            task2.Wait();   // ожидаем завершения задачи task2
            task3.Wait();   // ожидаем завершения задачи task3

            Console.WriteLine("Конец программы");
        }
    }
}
