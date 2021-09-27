using Prism.Mvvm;
using NoSqlManager.Contracts;
using NoSqlManager.Infrastructure.Contracts;
using NoSqlManager.Infrastructure.Models;
using System;

namespace NoSqlManager.Base
{
    public abstract class BaseConnectionViewModel<T> : BindableBase, IConnectionViewModel where T : IConnection
    {
        public Guid Id => this.Source.Id;
        public T Source { get; set; }
        public abstract ConnectionType Type { get; }

        public bool IsConnected => false;

        public BaseConnectionViewModel(T source)
        {
            this.Source = source;
        }

        public IConnection GetSource()
        {
            return this.Source;
        }

        public abstract void Connect();

        public abstract void Disconnect();
    }
}
