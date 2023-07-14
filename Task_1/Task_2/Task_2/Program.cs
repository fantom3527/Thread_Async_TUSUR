using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        private static bool flag = false;

        static void Main()
        {
            Task<int> task1 = new Task<int>(GetIntResult);
            task1.Start();
            Console.WriteLine($"Результат выполнения асинхронной операции (Int) #1 - {task1.Result}");
            Thread.Sleep(1000);

            TaskFactory taskFactory = new TaskFactory();
            Task<bool> task2 = taskFactory.StartNew(new Func<bool>(GetBoolResult));
            Console.WriteLine($"Результат выполнения асинхронной операции (Bool) #2 - {task2.Result}");
            Thread.Sleep(1000);
            
            Task<bool> task3 = Task.Run(new Func<bool>(GetBoolResult));
            Console.WriteLine($"Результат выполнения асинхронной операции (Bool) #3 - {task3.Result}");
        }

        private static int GetIntResult()
        {
            return 1;
        }

        private static bool GetBoolResult()
        {
            if (flag)
            {
                flag = false;
                return true;
            }
            else
            {
                flag = true;
                return false;
            }
        }
    }
}
