using System;

namespace Task_1
{
    class Program
    {
        static void Main()
        {
            //Первая часть задания
            Tasks tasks = new Tasks();

            //Выводит рандомные символы в разных потоках используя Thead. Здание №1
            tasks.SubTask_1();

            ////Выводит заданные символы в разных потоках используя ThreadPoolWorker. Здание №2
            //tasks.SubTask_2();

            //// Выводит возведенное в степень число в разных потоках используя ThreadPoolWorker с возвратом значения. Здание №3
            //tasks.SubTask_3();

            ////Выводит заданные и рандомные символы в разных потоках используя Task. Здание №4
            //tasks.SubTask_4();
        }
    }
}
