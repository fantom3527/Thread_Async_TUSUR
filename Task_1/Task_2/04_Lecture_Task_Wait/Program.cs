using System;
using System.Threading;
using System.Threading.Tasks;

namespace _04_Lecture_Task_Wait
{
    class Program
    {
        static void Main()
        {
            //Не советуются к исплоьзованию. Могут приводить к блокировкам (deadLocks)
            // Wait(), WaitAll(), WaitAny() и свойство Result

            Task[] tasks = new Task[]
            {
                new Task(DoSomething, 1000),
                new Task(DoSomething, 800),
                new Task(DoSomething, 2000),
                new Task(DoSomething, 1000),
                new Task(DoSomething, 3500),
            };

            Console.WriteLine($"Метод Main выполняется в потоке {Thread.CurrentThread.ManagedThreadId}") ;
            foreach (Task task in tasks)
                task.Start();

            Console.WriteLine($"Метод Main ожидает в потоке {Thread.CurrentThread.ManagedThreadId}");

        }

        private static void DoSomething(object sleepTime)
        {
            Console.WriteLine($"Задача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep((int)sleepTime);

            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
