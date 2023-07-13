using System;
using System.Threading;

namespace RepeatLecture
{
    class Program
    {
        public delegate void DelegateName(object id);

        static void Main(string[] args)
        {
            //Пример работы с делегатом
            //DelegateName _delegate = WriteString;
            //_delegate(3);

            //дот нет сама определяет, что должен быть делегат, когда мы кидаем просто функцию WriteString в Thread()
            // Будет примерно: Thread(parameterizedThreadStart(WriteString))
            //ThreadStart threadStart = WriteString;
            //ParameterizedThreadStart parameterizedThreadStart = WriteString;

            Thread thread = new Thread(WriteString);
            //чтобы установить приоритет, что поток не основной или основной.
            thread.IsBackground = true;

            Console.WriteLine("Для запуска нажмите любую клавишу.");
            Console.ReadKey();

            thread.Start("Task_2");

            for (int i = 0; i < 80; i++)
            {
                Console.WriteLine("Task_1");
            }

            Console.ReadLine();
        }

        private static void WriteString(object arg)
        {
            for (int i = 0; i < 80; i++)
            {
                Console.WriteLine($"\t{arg}");
            }
        }

        //для примера с ThreadStart
        //private static void WriteString()
        //{
        //    for (int i = 0; i < 80; i++)
        //    {
        //        Console.WriteLine($"\tБез параметров");
        //    }
        //}
    }
}
