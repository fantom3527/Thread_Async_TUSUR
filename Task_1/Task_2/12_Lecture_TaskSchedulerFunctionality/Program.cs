using System;
using System.Threading;
using System.Threading.Tasks;

namespace _12_Lecture_TaskSchedulerFunctionality
{
    class Program
    {
        // Планировщик задач - механизм который позволяет настроить выполнение задач заданным программистом способом.
        //-- В .Net для работы с планировщиком задач используется абстрактный класс TaskScheduler, т.к. абстрактный, то реализация планироващика полностью ложиться на программиста
        //-- Но .Net предоставляет некоторые стандартные планировщики задач, которые наследуются от TaskScheduler

        static void Main()
        {
            Console.SetWindowSize(100, 45);
            Console.WriteLine($"Id потока метода Main - {Thread.CurrentThread.ManagedThreadId}");

            Task[] tasks = new Task[10];
            ReviewTaskScheduler reviewTaskScheduler = new ReviewTaskScheduler();


            Console.WriteLine("Выберите из списка варианты продолжений:" +
                            "\n 1) Выполнить QueueTaskTesting" +
                            "\n 2) Выполнить TryExecuteTaskInLineTesting" +
                            "\n 2) Выполнить TryDequeueTesting");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Выполняется 1 пункт");

                        QueueTaskTesting(tasks, reviewTaskScheduler);

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Выполняется 2 пункт");

                        TryExecuteTaskInLineTesting(tasks, reviewTaskScheduler);

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Выполняется 3 пункт");

                        TryDequeueTesting(tasks, reviewTaskScheduler);

                        break;
                    }
                default:
                    break;
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch (Exception)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Несколько задач были отменены!");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine($"Метод Main закончил свое выполнение");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Вызов метода QueueTask.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="scheduler"></param>
        private static void QueueTaskTesting(Task[] tasks, TaskScheduler scheduler)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполниласьт в потоке {Thread.CurrentThread.ManagedThreadId}\n");
                });

                tasks[i].Start(scheduler);
            }
        }

        /// <summary>
        /// Вызов метода TryExecuteTaskInLine (ассинхронно). 
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="scheduler"></param>
        private static void TryExecuteTaskInLineTesting(Task[] tasks, TaskScheduler scheduler)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task<int>(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
                    return 1;
                });
            }

            foreach (var task in tasks)
            {
                task.Start(scheduler);
                task.Wait();
            }
        }

        /// <summary>
        /// Вызов метода TryDequeue.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="scheduler"></param>
        private static void TryDequeueTesting(Task[] tasks, TaskScheduler scheduler)
        {
            #region Скоординированная отмена

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            //Происходит отмена после 555 миллисекунд
            cts.CancelAfter(555);

            #endregion

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Задача {Task.CurrentId} выполнилась в потоке {Thread.CurrentThread.ManagedThreadId}");
                }, token);

                tasks[i].Start(scheduler);
            }
        }
    }
}
