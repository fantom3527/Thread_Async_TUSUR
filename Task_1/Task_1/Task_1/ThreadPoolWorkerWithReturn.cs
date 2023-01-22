using System;
using System.Threading;

namespace Task_1
{
    /// <summary>
    ///     Класс управление потоками без возвращения значения.
    /// </summary>
    class ThreadPoolWorkerWithReturn<TResult>
    {
        #region Private property

        //private readonly Action<object> _action;
        private readonly Func<object, object, TResult> _func;
        private TResult result;
        private int? _countThreadPool { get; set; } = null;

        #endregion

        #region Public property

        public bool Success { get; private set; }
        public bool Completed { get; private set; }
        public Exception Exception { get; private set; }
        public string ExceptionFull { get; set; }

        public TResult Result
        { 
            get
            {
                while (Completed == false && _countThreadPool != null && _countThreadPool != 0)
                    Thread.Sleep(150);

                return Success && Exception == null ? result : throw Exception;
            }
        }

        #endregion

        public ThreadPoolWorkerWithReturn(Func<object, object, TResult> func)// => this._action = action ?? throw new ArgumentNullException(nameof(action));
        {
            this._func = func ?? throw new ArgumentNullException(nameof(func));
            result = default;
            _countThreadPool = 0;
        }

        public void Start(object firstValue, object secondValue)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), new object[] { firstValue, secondValue });
        }

        public void Wait()
        {
            while (Completed == false && _countThreadPool != null && _countThreadPool != 0)
                Thread.Sleep(150);

            if (Exception != null)
                throw Exception;
        }

        private void ThreadExecution(object state)
        {
            try
            {
                object[] array = state as object[];
                _countThreadPool++;
                result = _func.Invoke(array[0], array[1]);
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

                if (_countThreadPool == 0)
                    Completed = true;
            }
        }

    }
}
