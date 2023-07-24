using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

// Async / await - ключевые слова, которые помогают писать асанхронный код как синхронный.

//-- Async:
//--- Является модификатором для методов при определении.
//--- Указывает что метод является асинхронным.
//--- Также позволяет использовать ключевое слово await в методе.
//--- Указывает компилятору на создание конечного автомата для обеспечения работы асинхронного метода.

//-- Await:
//--- Является унарным оператором.
//--- Говорит, что необходимо дождаться завершения выполнения асинхронной операции.
//--- Создание продолжения.
//--- Не блокирует вызывающий поток

//--- !!! Для применения оператора к какому-то экземпляру типа, тип данного экземлпяра должен обладать Следующим функционалом:
//---// Иметь доступный метод GetAwaiter(), возвращаемый этим методом, Пример:
//---//- Task task = Task.Run(() => WriteChar('$'));
//---//- TaskAwaiter taskAwaiter = task.GetAwaiter();
//---//-- Возвращаемый этим методом экземпляр должен:
//---//--- 1 Реализовывать интерфейсы ICriticalNotifyCompletion и INotifyCompletion.
//---//--- 2 Иметь свойство bool IsCompleted { get; } - доступен только для чтения.
//---//--- 3 Иметь метод GetResult().

// Асинхронные методы - методы использующие ключевые слова async/await (или более сложные конструкции) и имеют спец. тип возвращаемого значения.
//-- Типы возвращаемых значений асинхронного метода:
///--- 1. Task/Task<TResult>.
///--- 2. ValueTask/ValueTask<TResult>.
///--- 3. void - для обработчиков событий.
///--- 4. Пользовательский тип.

// Awaitable методы - это ассинхронные методы, завршение которых можно подождать (обычный ассинхронный метод)

namespace Task_3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine($"Метод Main начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            //Подчеркивает, потому что метод WriteCharAsync() не ожидаемый тк метод Main не ассинхронный - значит он может завешриться раньше, чем WriteCharAsync
            WriteCharAsync('#');
            WriteChar('*');

            Console.WriteLine($"Метод Main закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }

        /// <summary>
        /// Запустит во вторичном потоке для паралельного выполнения.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private static async Task WriteCharAsync(char symbol)
        {
            Console.WriteLine($"Метод WriteCharAsync начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() => WriteChar(symbol));

            Console.WriteLine($"Метод WriteCharAsync закончил свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void WriteChar(char symbol)
        {
            Console.WriteLine($"Метод WriteChar начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId} Id задачи - {Task.CurrentId}");
            Thread.Sleep(500);

            for (int i = 0; i < 80; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(100);
            }
        }
    }
}
