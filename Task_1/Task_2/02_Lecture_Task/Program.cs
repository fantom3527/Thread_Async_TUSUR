using System;
using System.Threading;
using System.Threading.Tasks;

namespace _02_Lecture_Task
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine($"Task Id метода Main : {Task.CurrentId ?? -1}");
            Console.WriteLine($"Thread Id метода Main : {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));

            // Параметр TaskCreationOptions.PreferFairness означает, что Task отправленный ранььше, выполняется раньше. (рекомендация для планировщика задач)
            // Параметр TaskCreationOptions.LongRunning - подсказка планировщику, что задача будет выполняться долго - значит лучше создать отдельный поток под эту задачу
            Task task = new Task(new Action(DoSomething), TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning);

            // Поведение задач по умолчанию.
            //TaskCreationOptions.None
            //TaskCreationOptions.PreferFairness
            //TaskCreationOptions.LongRunning
            // Задача должна быть присоединена к родительской
            //TaskCreationOptions.AttachedToParent
            // Любая дочерняя задача не может быть присоединена к родительской задаче
            //TaskCreationOptions.DenyChildAttach
            // Заставляет дочерние задачи использовать планировщик задач по умолчанию
            //TaskCreationOptions.HideScheduler
            // Ассинхронно должны выполняться продолжения
            //TaskCreationOptions.RunContinuationsAsynchronously

            task.Start();
            Thread.Sleep(50);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Метод Main выполняется.");
                Thread.Sleep(100);
            }

            Console.ReadKey();
        }

        private static void DoSomething()
        {
            Console.WriteLine($"Task Id метода DoSomething : {Task.CurrentId ?? -1}");
            Console.WriteLine($"Thread Id метода DoSomething : {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(new string('-', 80));
        }
    }
}
