using System;
using System.Threading;
using System.Threading.Tasks;

namespace _08_Lecture_Task_Continuation_DifferentWays
{
    class Program
    {
        static void Main()
        {
            Task<int> task = Task.Run<int>(new Func<int>(GetValue));

            // Запускаться будут друг за другом.
            task.ContinueWith(Increment)
                .ContinueWith(Increment)
                .ContinueWith(Increment)
                .ContinueWith(Increment)
                .ContinueWith(Increment)
                .ContinueWith(ShowRes);

            Console.WriteLine("Метод Main завершил свою работу..");
            Console.ReadKey();
        }

        private static int GetValue() => 10;

        private static int Increment(Task<int> task)
        {
            Console.WriteLine($"Продолжение task Id #{Task.CurrentId} Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            int result = task.Result + 1;
            return result;
        }

        private static void ShowRes(Task<int> t)
        {
            Console.WriteLine($"Продолжение task Id #{Task.CurrentId} Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Результат - {t.Result}");
        }
    }
}
