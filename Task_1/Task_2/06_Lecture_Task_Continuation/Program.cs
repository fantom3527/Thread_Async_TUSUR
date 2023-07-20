using System;
using System.Threading;
using System.Threading.Tasks;

namespace _06_Lecture_Task_Continuation
{
    class Program
    {
        //Продолжение - представляет собой способ настройки задачи таким образом, что после завершения, задача должна продолжиться и выполнить какой-то другой метод
        static void Main()
        {
            Task task = new Task(new Action<object>(OperationAsync), "Hello word");
            Task continuation = task.ContinueWith(Continuation);

            Console.WriteLine($"Статус продолжения - {continuation.Status}");

            task.Start();

            Console.ReadKey();
        }

        private static void OperationAsync(object arg)
        {
            Console.WriteLine($"Задача #{Task.CurrentId} началось в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Argument value - {arg.ToString()}");
            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Continuation(Task task)
        {
            Console.WriteLine($"Продолжение #{Task.CurrentId} сработало в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Параметр задачи - {task.AsyncState}");
            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке сразу после выполнения задачи {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
