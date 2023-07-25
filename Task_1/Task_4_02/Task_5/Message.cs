using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Task_5
{

    // Класс используется как сообщение, котрое будет передаваться из одного потока в другой.
    // В нем описан делегат, который необходимо будет выполнять в контексте другого потока.

    class Message
    {
        // Хранятся делегаты для выполнения м методе main.
        // Методы Post и Send принимают именно такой тип делегата.
        public SendOrPostCallback Callback { get; set; }
        
        // Согласно сигнатуре делегата методы в CallBack должны принимать один параметр.
        public object State { get; set; }

        public Message(){ }

        public Message(SendOrPostCallback callback, object state) 
        {
            Callback = callback;
            State = state;
        }
    }
}
