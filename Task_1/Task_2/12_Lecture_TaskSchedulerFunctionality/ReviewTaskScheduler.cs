using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace _12_Lecture_TaskSchedulerFunctionality
{
    internal class ReviewTaskScheduler : TaskScheduler
    {
        private readonly LinkedList<Task> tasksList = new LinkedList<Task>();

        protected override IEnumerable<Task> GetScheduledTasks() => tasksList;

        /// <summary>
        /// Метод добавления задачи вызывается методом Start класса Task.
        /// </summary>
        /// <param name="task">Задача, которую добавляем</param>
        protected override void QueueTask(Task task)
        {
            Console.WriteLine($"[QueueTask] Задача #{task.Id} поставлена в очередь..");
            tasksList.AddLast(task);
            ThreadPool.QueueUserWorkItem(ExecuteTasks, null);
        }

        /// <summary>
        /// Метод пытается выполнить задачу синхронно. Вызывается методами ожидания, к примеру Wait, WaitAll
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskWasPreviouslyQueued"></param>
        /// <returns></returns>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.WriteLine($"[TryExecuteTaskInline] Попытка выполнить задачу #{task.Id} синхронно..");

            lock (tasksList)
            {
                tasksList.Remove(task);
            }

            return base.TryExecuteTask(task);
        }

        /// <summary>
        /// Пытается удалить задачу из очереди. Вызывается при отмене выполнения задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        protected override bool TryDequeue(Task task)
        {
            Console.WriteLine($"[TryDequeue] Попытка удалить задачу {task.Id} из очереди...");
            bool result = false;

            lock (tasksList)
            {
                result = tasksList.Remove(task);
            }

            if (result == true)
            {
                Console.WriteLine($"[TryDequeue] задача {task.Id} была удалена из очереди на выполнение...");
            }

            return result;
        }

        private void ExecuteTasks(object _)
        {
            while (true)
            {
                //Thread.Sleep(2000);
                Task task = null;

                lock (tasksList)
                {
                    if (tasksList.Count == 0)
                    {
                        break;
                    }

                    task = tasksList.First.Value;
                    tasksList.RemoveFirst();
                }

                if (task == null)
                {
                    break;
                }

                base.TryExecuteTask(task);
            }
        }



    }
}
