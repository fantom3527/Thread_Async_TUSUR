using System;
using System.Threading;
using System.Threading.Tasks;

namespace _002_Lecture_AsyncAwait
{

    //Пример с ассинхронным методом Mainи ожиданием метода WriteCharAsync

    class Program
    {
        static async Task Main()
        {
            Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            //Подчеркивает, потому что метод WriteCharAsync() не ожидаемый тк метод Main не ассинхронный - значит он может завешриться раньше, чем WriteCharAsync
            await WriteCharAsync('#');
            WriteChar('*');

            Console.WriteLine($"Метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }

        /// <summary>
        /// Запустит во вторичном потоке для паралельного выполнения.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Метод WriteCharAsync начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() => WriteChar(symbol));

            Console.WriteLine($"Метод WriteCharAsync закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine($"Метод WriteChar начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId} Id задачи - {Task.CurrentId}");
            Thread.Sleep(500);

            for (int i = 0; i < 80; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(100);
            }
        }
    }
}
