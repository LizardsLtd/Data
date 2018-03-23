using System;

namespace Lizards.Data.Events
{
    public sealed class ExceptionEvent : EventBase
    {
        public ExceptionEvent(Exception exp)
            : this(exp, exp.Message)
        {
        }

        public ExceptionEvent(Exception exp, string message)
        {
            this.Exception = exp;
            this.Message = message;
        }

        public Exception Exception { get; }

        public string Message { get; }
    }
}