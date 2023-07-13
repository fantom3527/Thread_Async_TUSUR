using System;
using System.Threading;

namespace ThreadPool_Lecture_04
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Для запуска нажмите любую клавишу.");
            Console.ReadKey();

            ThreadPoolWorker<int> threadPoolWorker = new ThreadPoolWorker<int>(SumNumber);
            threadPoolWorker.Start(300);

            while (threadPoolWorker.Completed == false)
            {
                Console.WriteLine("*");
                Thread.Sleep(35);
            }

            Console.WriteLine();
            Console.WriteLine($"Результат ассинхронной операции = {threadPoolWorker.Result:N}");
        }

        private static int SumNumber(object arg)
        {
            int number = (int)arg;
            int sum = 0;

            for (int i = 0; i < number; i++)
            {
                sum += i;
                Thread.Sleep(1);
            }

            return sum;
        }
    }
}
