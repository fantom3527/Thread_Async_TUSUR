using System;
using System.Threading;
using System.Threading.Tasks;

namespace _10_Lecture_Task_Continuation_Parametrs
{
    class Program
    {
        static void Main()
        {
            Task task = Task.Run(() => Method());

            // Разные условия с помощью параметров.
            task.ContinueWith((t) => Continuation(t), TaskContinuationOptions.RunContinuationsAsynchronously);

            Console.ReadKey();
        }

        private static void Method()
        {
            Thread.Sleep(2000);
            Console.WriteLine($"Задача #{Task.CurrentId} выполнила метод в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));
        }

        private static void Continuation(Task task)
        {
            Console.WriteLine($"Id задачи продолжения - {Task.CurrentId}");
            Console.WriteLine($"Продолжение выполнилось в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));
        }
    }
}
