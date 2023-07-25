using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Task_5
{
    internal class ConsoleSynchronizationContext : SynchronizationContext
    {
        // Выполняется перед началом операции.
        public override void OperationStarted()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[ConsoleSynchronizationContext] Операция начата");
            Console.ResetColor();
        }

        // Выполняется после выполнения операции
        public override void OperationCompleted()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[ConsoleSynchronizationContext] Операция завершена");
            Console.ResetColor();
        }

        /// <summary>
        /// Передача сообщения асинхронно.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public override void Post(SendOrPostCallback d, object state)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[ConsoleSynchronizationContext] Post выполнился");
                Console.ResetColor();

                Message message = new Message(d, state);
                MessageListenter.AddMessage(message);
            });
        }

        /// <summary>
        /// Получение для синхронной работы.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="state"></param>
        public override void Send(SendOrPostCallback d, object state)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[ConsoleSynchronizationContext] Send выполнился");
            Console.ResetColor();

            Message message = new Message(d, state);
            MessageListenter.AddMessage(message);
        }
    }
}
