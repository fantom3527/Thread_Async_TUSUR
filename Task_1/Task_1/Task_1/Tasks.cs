using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1
{
    class Tasks
    { 
        /// <summary>
        ///     Выводит рандомные символы в разных потоках используя Thead. Здание №1
        /// </summary>
        public void SubTask_1()
        {
            Thread threadFirst = new Thread(WhriteRandomSymbol);
            Thread threadSecond = new Thread(WhriteRandomSymbol);

            threadFirst.Start("threadFirst");
            threadSecond.Start("threadSecond");
        }

        /// <summary>
        ///     Для себя делал (пример из лекции).
        /// </summary>
        public void SubTask()
        {
            Report();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(WhriteRandomSymbol));
            ThreadPool.QueueUserWorkItem(new WaitCallback(WhriteRandomSymbol));
            Report();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ExampleSecond));
            Report();

            Console.ReadKey();
        }

        /// <summary>
        ///      Выводит заданные символы в разных потоках используя ThreadPoolWorker. Здание №2
        /// </summary>
        public void SubTask_2()
        {
            try
            {
                ThreadPoolWorker threadPoolWorker = new ThreadPoolWorker(new Action<object>(WhriteSymbolRandomNumber));                
                threadPoolWorker.Start("@");
                threadPoolWorker.Start("%");
                threadPoolWorker.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex}");
            }

            Console.WriteLine("Метод SubTask_2 закончил свою работу.");
        }

        /// <summary>
        ///     Выводит возведенное в степень число в разных потоках используя ThreadPoolWorker с возвратом значения. Здание №3
        /// </summary>
        public void SubTask_3()
        {
            ThreadPoolWorkerWithReturn<int> threadPoolWorkerWithReturn = new ThreadPoolWorkerWithReturn<int>(PowValue);
            threadPoolWorkerWithReturn.Start(34, 3);

            while (threadPoolWorkerWithReturn.Completed == false)
            {
                Console.Write("+");
                Thread.Sleep(30);
            }

            Console.WriteLine();
            Console.WriteLine($"Результат выполнения операции = {threadPoolWorkerWithReturn.Result}");
            Console.ReadKey();
        }

        /// <summary>
        ///     Выводит заданные и рандомные символы в разных потоках используя Task. Здание №4
        /// </summary>
        public void SubTask_4()
        {
            Task.Run(() => WhriteRandomSymbol(null));
            Task.Run(() => WhriteSymbolRandomNumber('F'));

            Console.ReadKey();
        }

        /// <summary>
        ///     Вывести в консоль рандомный символ.
        /// </summary>
        private static void WhriteRandomSymbol(object _)
        {
            Random random = new Random();
            int sleep = random.Next(300, 1000);
            for (int i = 0; i < 10; i++)
            {
                char randomSymbol = (char)(random.Next(26) + 65);
                Console.WriteLine(randomSymbol + $" Номер потока: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(sleep);
            }
        }

        /// <summary>
        ///     Вывести в консоль символ рандомное количество раз.
        /// </summary>
        private static void WhriteSymbolRandomNumber(object symbol)
        {
            Random random = new Random();
            int RandomNumber = random.Next(4, 14);
            int sleep = random.Next(300, 1500);
            for (int i = 0; i < RandomNumber; i++)
            {
                Console.WriteLine(symbol.ToString() + $" Номер потока: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(sleep);
            }
        }

        /// <summary>
        ///     Возвести в степень число.
        /// </summary>
        /// <param name="value">Число.</param>
        /// <param name="power">Число в какую степень необходимо возвести value.</param>
        /// <returns>Возведенное в степень число.</returns>
        private static int PowValue(object value, object power)
        {
            Thread.Sleep(1000);

            return (int)Math.Pow((int)value, (int)power);
        }

        private static void ExampleFirst(object _)
        {
            Console.WriteLine($"Метод ExampleFirst начал выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
            Console.WriteLine($"Метод ExampleFirst закончил выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void ExampleSecond(object _)
        {
            Console.WriteLine($"Метод ExampleSecond начал выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Метод ExampleSecond закончил выполняться в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        private void Report()
        {
            ThreadPool.GetMaxThreads(out int maxWorkerTherads, out int maxPortThreads);
            ThreadPool.GetAvailableThreads(out int workerTherds, out int portThreads);

            Console.WriteLine($"Рабочие потоки {workerTherds} из {maxWorkerTherads}");
            Console.WriteLine($"IO потоки {portThreads} из {maxPortThreads}");
            Console.WriteLine(new string('-', 80));
        }

    }
}
