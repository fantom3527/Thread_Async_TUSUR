using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace _11_Lecture_Task_NestedTasks
{

    // Вложенная задача - которая была создана внутри другой задачи и выполняется независимо от задачи внутри которой она была создана.
    //-- Задача может иметь неограниченное количество вложенных задач, ограничевается вложенными ресурсами.
    //-- Родительская задача не будет ожидать окончания вложенных задач.

    // Дочерняя задача - та, которая была создана с параметром TaskCreationOprtion.AttachedToParant.
    //-- Родительская задача не будет завершена (не вернет результат) до тех пор, пока все ее дочерние задачи не будут выполнены.

    // Task.Run() - создается по умолчанию с параметром TaskCreationOptions.DenyCheldAttach, поэтому если необходимы дочернии, надо указать параметр TaskCreationOprtion.AttachedToParant.

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Выберите из списка варианты продолжений:" +
                            "\n 1) Вложенные задачи" +
                            "\n 2) Дочерние задачи" +
                            "\n 2) Дочерние задачи с продолжением");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Выполняется 1 пункт");

                        Task parent = new Task(() =>
                        {
                            new Task(() =>
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine("Nested task 1");
                            }).Start();

                            new Task(() =>
                            {
                                Thread.Sleep(2000);
                                Console.WriteLine("Nested task 2");
                            }).Start();

                            Thread.Sleep(200);
                        });

                        // Родительская задача не будет ожидать окончания вложенных задач.
                        parent.Start();
                        parent.Wait();

                        Console.WriteLine("Ended");
                        Console.WriteLine(new string('-', 80));
                        Console.ReadKey();

                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Выполняется 2 пункт");

                        Task parent = new Task(() =>
                        {
                            new Task(() =>
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine("Child task 1");
                            }, TaskCreationOptions.AttachedToParent).Start();

                            new Task(() =>
                            {
                                Thread.Sleep(2000);
                                Console.WriteLine("Child task 2");
                            }, TaskCreationOptions.AttachedToParent).Start();

                            Thread.Sleep(200);
                        }); // если добавить для родительской задачи параметр TaskCreationOptions.DenyCheldAttach - то дочерние задачи перестанут быть такими несмотря на TaskCreationOptions.AttachedToParent
                            // И родительская задача не будет ждать свои задачи.

                        // Родительская задача будет ожидать окончания вложенных задач.
                        parent.Start();
                        parent.Wait();

                        Console.WriteLine("Ended");
                        Console.WriteLine(new string('-', 80));
                        Console.ReadKey();

                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Выполняется 3 пункт");

                        Task<string> parent = new Task<string>(() =>
                        {
                            Task<int> task1 = new Task<int>(() => Add(5000), TaskCreationOptions.AttachedToParent);
                            Task<int> task2 = new Task<int>(() => Add(10000), TaskCreationOptions.AttachedToParent);
                            Task<int> task3 = new Task<int>(() => Add(20000), TaskCreationOptions.AttachedToParent);

                            task1.Start();
                            task2.Start();
                            task3.Start();

                            // Для продолженя дочерней задачи
                            task1.ContinueWith((t) => Console.WriteLine($"{Task.CurrentId} {t.Result}"));
                            task2.ContinueWith((t) => Console.WriteLine($"{Task.CurrentId} {t.Result}"));
                            task3.ContinueWith((t) => Console.WriteLine($"{Task.CurrentId} {t.Result}"));

                            return "Parent task ended";
                        });

                        parent.Start();

                        Console.WriteLine($"Результат: {parent.Result}");
                        Console.WriteLine(new string('-', 80));
                        Console.ReadKey();

                        break;
                    }
                default:
                    break;
            }

        }

        static int Add(int value)
        {
            Console.WriteLine(Task.CurrentId);
            int result = 0;
            Thread.Sleep(2000);

            for (int i = 0; i < value; i++)
            {
                result += i;
            }

            return result;
        }
    }
}
