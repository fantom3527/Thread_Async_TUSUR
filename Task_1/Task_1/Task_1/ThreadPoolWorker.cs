using System;
using System.Threading;

namespace Task_1
{
    /// <summary>
    ///     Класс управление потоками без возвращения значения.
    /// </summary>
    class ThreadPoolWorker
    {
        #region Private property

        private readonly Action<object> _action;
        private int? _countThreadPool { get; set; } = null;

        #endregion

        #region Public property

        public bool Success { get; private set; }
        public bool Completed { get; private set; }
        public Exception Exception { get; private set; }
        public string ExceptionFull { get; set; }

        #endregion

        public ThreadPoolWorker(Action<object> action)// => this._action = action ?? throw new ArgumentNullException(nameof(action));
        {
            this._action = action ?? throw new ArgumentNullException(nameof(action));
            _countThreadPool = 0;
        }

        public void Start(object symbol)
        {
            _countThreadPool++;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), symbol);
        }                 

        public void Wait()
        {
            while (Completed == false && _countThreadPool != null && _countThreadPool != 0)
                Thread.Sleep(150);
           
            if (Exception != null)
                throw Exception;
        }

        private void ThreadExecution(object symbol)
        {
            try
            {
                _action.Invoke(symbol);
                Success = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                ExceptionFull += $"Ошибка в потоке с номером: {Thread.CurrentThread.ManagedThreadId} - {Exception} \n\n";
                Success = false;
            }
            finally
            {
                if (_countThreadPool != 0 && _countThreadPool != null)
                    _countThreadPool--;

                if(_countThreadPool == 0)
                    Completed = true;
            }
        }

    }
}
