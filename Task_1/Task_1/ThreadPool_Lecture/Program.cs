using System;
using System.Threading;

namespace ThreadPool_Lecture
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Для запуска нажмите любую кнопку.");
            Console.ReadKey();

            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Example1));
            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Example2));
            Report();

            Console.ReadKey();
            Report();
        }

        private static void Example1(object state)
        {
            Console.WriteLine($"Метод Example1 начал выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"Метод Example1 закончил выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Example2(object state)
        {
            Console.WriteLine($"Метод Example2 начал выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Метод Example2 закончил выполнятся в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Report()
        {
            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxPortThreads);
            // количество поток доступные для использования
            ThreadPool.GetAvailableThreads(out int workerThreads, out int portThreads);

            Console.WriteLine($"рабочие потоки {workerThreads} из {maxWorkerThreads}");
            Console.WriteLine($"IO потоки {portThreads} из {maxPortThreads}");
            Console.WriteLine(new string('-', 80));
        }
    }
}
