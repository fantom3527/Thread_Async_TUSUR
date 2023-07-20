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

            Console.WriteLine("Выберите из списка:" +
                             "\n 1) Ждет task.Wait(); в foreach для всех tasks" +
                             "\n 2) Ждет Task.WaitAll(tasks)" +
                             "\n 3) Ждет Task.WaitAny(tasks)"); 
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Метод Main выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            foreach (Task task in tasks)
                task.Start();

            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Выполняется 1 пункт");
                        Console.WriteLine($"Метод Main ожидает в потоке {Thread.CurrentThread.ManagedThreadId}");

                        foreach (Task task in tasks)
                            task.Wait();
                        WriteSomething();

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Выполняется 2 пункт");
                        Console.WriteLine($"Метод Main ожидает в потоке {Thread.CurrentThread.ManagedThreadId}");

                        Task.WaitAll(tasks);
                        WriteSomething();

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Выполняется 3 пункт");
                        Console.WriteLine($"Метод Main ожидает в потоке {Thread.CurrentThread.ManagedThreadId}");

                        Task.WaitAny(tasks);
                        WriteSomething();

                        break;
                    }
                default:
                    break;
            }
        }

        private static void DoSomething(object sleepTime)
        {
            Console.WriteLine($"Задача #{Task.CurrentId} началась в потоке {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep((int)sleepTime);

            Console.WriteLine($"Задача #{Task.CurrentId} завершилась в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void WriteSomething()
        {
            Console.WriteLine($"Метод Main продолжает в потоке {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Main {i}");
            }
        }
    }
        
}
