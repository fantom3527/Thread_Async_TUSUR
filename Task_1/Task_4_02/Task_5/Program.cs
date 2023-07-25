using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_5
{

    // Контекст синхронизации - механизм, который позволяет возобновить выполнение кода в конкретном потоке (в котором оно было начато) в c# представлен классом SyncheonizationContext.

    class Program
    {
        static Program()
        {
            // Установка контекста синхронизации.
            // Если закоментировать SynchronizationContext, то выполняется правило 3 - если не получилось найти ни контекста синхронизации, ни планировщика задач
            // То система пытается выполнить продолжение либо синхронно, либо через
            // SynchronizationContext.SetSynchronizationContext(new ConsoleSynchronizationContext());
        }

        static void Main()
        {
            Message message = new Message(ApplicationMain, null);
            MessageListenter.AddMessage(message);

            new MessageListenter().Listen();

            Console.ReadKey();
        }

        /// <summary>
        /// Происходит основной функционал.
        /// </summary>
        /// <param name="_"></param>
        private async static void ApplicationMain(object _)
        {
            Console.WriteLine($"Наш метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            StubMethod_1();

            await MethodAsync();

            StubMethod_2();

            Console.WriteLine($"Наш метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task MethodAsync()
        {
            Console.WriteLine($"{new string('-', 80)}");

            Console.WriteLine($"[MethodAsync] Код ДО оператора await выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(Method);
            Console.WriteLine($"[MethodAsync] Код ПОСЛЕ оператора await выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine($"{new string('-', 80)}");
        }

        private static void StubMethod_1()
        {
            Console.WriteLine($"Пример метода StubMethod_1 в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void StubMethod_2()
        {
            Console.WriteLine($"Пример метода StubMethod_2 в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void Method()
        {
            Console.WriteLine($"Метод Method был выполнен в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
