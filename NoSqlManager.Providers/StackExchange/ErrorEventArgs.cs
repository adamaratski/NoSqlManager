using System;

namespace NoSqlManager.Providers.StackExchange
{
    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public ErrorEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
