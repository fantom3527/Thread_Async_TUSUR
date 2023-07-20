using System;
using System.Threading;
using System.Threading.Tasks;

namespace _07_Lecture_Task_Continuation
{
    class Program
    {
        static void Main()
        {
            int a = 2, b = 3;

            Console.WriteLine("Выберите из списка варианты продолжений:" +
                            "\n 1) Через отдельный метод Continuation" +
                            "\n 2) Через анонимную функцию");
     
            int choice = Convert.ToInt32(Console.ReadLine());
            Task<int> task = Task.Run<int>(() => Calc(a, b));

            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Выполняется 1 пункт");

                        task.ContinueWith(Continuation);

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Выполняется 2 пункт");

                        task.ContinueWith((t) =>
                        {
                            Console.WriteLine($"Продолжение через лямду task Id #{Task.CurrentId} Thread Id #{Thread.CurrentThread.ManagedThreadId}");
                            Console.WriteLine($"Результат асинхронной операции через лямду - {task.Result}");
                        });

                        break;
                    }
                default:
                    break;
            }

            Console.ReadKey();
        }

        private static int Calc(int a, int b)
        {
            Console.WriteLine($"Task Id #{Task.CurrentId} Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            return a + b;
        }

        private static void Continuation(Task<int> task)
        {
            Console.WriteLine($"Продолжение task Id #{Task.CurrentId} Thread Id #{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Результат асинхронной операции - {task.Result}");
        }
    }
}
