using System;
using System.Threading;
using System.Threading.Tasks;

namespace _003_Lecture_AsyncAwait
{
    class Program
    {
        static void Main()
        {
            int x = 3, y = 5;

            Task<int> additionTask = AdditionAsync("[Асинхронно]", x, y);
            int syncSum = Addition("[Синхронно]", x, y);
            int asyncSum = 0;

            // Разные способы получения раезультата из асинхронной задачи:
            asyncSum = additionTask.Result;
            //asyncSum = additionTask.GetAwaiter().GetResult();
            //asyncSum = await additionTask;

            Console.WriteLine($"Результат асинхронного выполнения: {asyncSum}");
            Console.WriteLine($"Результат синхронного выполнения: {syncSum}");
            Console.WriteLine($"Метод Main завершил свою работу");
            Console.ReadKey();
        }

        private static int Addition(string operationName, int x, int y)
        {
            Console.WriteLine($"Метод Addition вызван: {operationName} ,начал свою работу в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);

            return x + y;
        }
        
        private static async Task<int> AdditionAsync(string operationName, int x, int y)
        {
            // Первый способ.
            int result = await Task.Run<int>(() => Addition(operationName, x, y));
            return result;

            // Второй способ.
            //return await Task.Run<int>(() => Addition(operationName, x, y));

            // Ошибочный способ.
            //return Task.Run<int>(() => Addition(operationName, x, y));
        }
    }
}
