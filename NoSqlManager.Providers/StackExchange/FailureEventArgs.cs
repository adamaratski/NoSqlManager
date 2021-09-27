using System;

namespace NoSqlManager.Providers.StackExchange
{
    public class FailureEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public FailureEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
