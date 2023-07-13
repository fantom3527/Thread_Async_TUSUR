using System;

namespace Task_1
{
    class Program
    {
        static void Main()
        {
            Tasks tasks = new Tasks();

            Console.WriteLine("Выберите из списка:" +
                             "\n 1) Выводит рандомные символы в разных потоках используя Thead. Здание №1" +
                             "\n 2) Выводит заданные символы в разных потоках используя ThreadPoolWorker. Здание №2" +
                             "\n 3) Выводит возведенное в степень число в разных потоках используя ThreadPoolWorker с возвратом значения. Здание №3" +
                             "\n 4) Выводит заданные и рандомные символы в разных потоках используя Task. Здание №4");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Выполняется 1 пункт");

                        tasks.SubTask_1();

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Выполняется 2 пункт");

                        tasks.SubTask_2();

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Выполняется 3 пункт");

                        tasks.SubTask_3();

                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Выполняется 4 пункт");

                        tasks.SubTask_4();

                        break;
                    }

                default:
                    break;
            }
        }
    }
}
