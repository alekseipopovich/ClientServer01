using System;
using System.Threading;


namespace MultiThread01
{

    class ProgrammLab03
    {
        // Общие переменные для работы потоков
        private static double sharedValue = 0.5; // Начальное значение
        private static readonly object lockObj = new object();
        private static bool cosTurn = true; // Флаг для определения чья очередь работать
        private static bool running = true; // Флаг для управления работой потоков
        private const int MaxIterations = 10; // Максимальное количество итераций
        private static int iterationCount = 0; // Счетчик итераций

        static void MainLab03()
        {
            Console.WriteLine($"Начальное значение: {sharedValue}");
            Console.WriteLine($"Максимальное количество итераций: {MaxIterations}");

            // Создаем и запускаем потоки
            Thread cosThread = new Thread(CalculateCosine);
            Thread acosThread = new Thread(CalculateArccosine);

            cosThread.Start();
            acosThread.Start();

            // Ждем завершения потоков
            cosThread.Join();
            acosThread.Join();

            Console.WriteLine("Программа завершена.");
        }

        // Метод для вычисления косинуса
        static void CalculateCosine()
        {
            sharedValue = Math.Cos(sharedValue*2);
            try
            {
                while (running)
                {
                    lock (lockObj)
                    {
                        // Проверяем условие выхода
                        if (!running || iterationCount >= MaxIterations)
                        {
                            running = false;
                            Monitor.Pulse(lockObj);
                            break;
                        }

                        // Ждем своей очереди
                        while (!cosTurn)
                        {
                            Monitor.Wait(lockObj);
                            if (!running) break;
                        }

                        if (!running) break;

                        // Вычисляем косинус
                        sharedValue = Math.Cos(sharedValue);
                        iterationCount++;
                        Console.WriteLine($"Поток косинуса [{iterationCount}/{MaxIterations}]: новое значение = {sharedValue:F6}");

                        // Проверяем выход за допустимые пределы
                        if (sharedValue < -1.0 || sharedValue > 1.0)
                        {
                            Console.WriteLine("Значение вышло за пределы [-1, 1] для арккосинуса");
                            running = false;
                            Monitor.Pulse(lockObj);
                            break;
                        }

                        // Передаем очередь другому потоку
                        cosTurn = false;
                        Monitor.Pulse(lockObj);
                    }

                    Thread.Sleep(300); // Пауза для наглядности
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Поток косинуса прерван");
            }
        }

        // Метод для вычисления арккосинуса
        static void CalculateArccosine()
        {
            sharedValue = Math.Acos(sharedValue);
            try
            {
                while (running)
                {
                    lock (lockObj)
                    {
                        // Проверяем условие выхода
                        if (!running || iterationCount >= MaxIterations)
                        {
                            running = false;
                            Monitor.Pulse(lockObj);
                            break;
                        }

                        // Ждем своей очереди
                        while (cosTurn)
                        {
                            Monitor.Wait(lockObj);
                            if (!running) break;
                        }

                        if (!running) break;

                        // Проверяем, что значение в допустимом диапазоне для арккосинуса
                        if (sharedValue < -1.0 || sharedValue > 1.0)
                        {
                            Console.WriteLine("Ошибка: значение вне диапазона для арккосинуса");
                            running = false;
                            Monitor.Pulse(lockObj);
                            break;
                        }

                        // Вычисляем арккосинус
                        sharedValue = Math.Acos(sharedValue);
                        iterationCount++;
                        Console.WriteLine($"Поток арккосинуса [{iterationCount}/{MaxIterations}]: новое значение = {sharedValue:F6}");

                        // Передаем очередь другому потоку
                        cosTurn = true;
                        Monitor.Pulse(lockObj);
                    }

                    Thread.Sleep(300); // Пауза для наглядности
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Поток арккосинуса прерван");
            }
        }
    }
}