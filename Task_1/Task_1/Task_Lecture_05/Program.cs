using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Lecture_05
{
    class Program
    {
        static void Main()
        {
            Action threadOutput = new Action(ThreadOutput);

            Console.WriteLine("Выберите из списка:" +
                              "\n 1) Выполнить через task.Start()" +
                              "\n 2) Выполнить через taskFactory.StartNew()" +
                              "\n 3) Выполнить через Task.Run()" +
                              "\n 4) Выполнить через task.RunSynchronously()");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Выполняется 1 пункт");

                        Task task = new Task(threadOutput);
                        task.Start();
                        MainOutput();

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Выполняется 2 пункт");

                        TaskFactory taskFactory = new TaskFactory();// можно через: Task.Factory
                        taskFactory.StartNew(threadOutput);
                        MainOutput();

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Выполняется 3 пункт");

                        Task.Run(threadOutput);

                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Выполняется 4 пункт");

                        Task task = new Task(threadOutput);
                        //Выполняется синхронно
                        task.RunSynchronously();
                        MainOutput();

                        break;
                    }

                default:
                    break;
            }
        }

        private static void ThreadOutput()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write('*');
                Thread.Sleep(75);
            }
        }

        private static void MainOutput()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write('!');
                Thread.Sleep(75);
            }
        }
    }
}
