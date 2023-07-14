using System;
using System.Threading;
using System.Threading.Tasks;

namespace _03_Lecture_Task
{
    class Program
    {
        static void Main()
        {
            Task task = new Task(new Action(Method));

            Console.WriteLine(task.Status);

            task.Start();

            Console.WriteLine(task.Status);
            Thread.Sleep(1000);

            Console.WriteLine(task.Status);
            Thread.Sleep(2000);

            Console.WriteLine(task.Status);
        }

        private static void Method()
        {
            Thread.Sleep(2000);
        }
    }
}
