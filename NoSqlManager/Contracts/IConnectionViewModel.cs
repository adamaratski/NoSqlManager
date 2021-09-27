using NoSqlManager.Infrastructure.Contracts;
using NoSqlManager.Infrastructure.Models;
using System;

namespace NoSqlManager.Contracts
{
    public interface IConnectionViewModel 
    {
        Guid Id { get; }
        ConnectionType Type { get; }
        IConnection GetSource();
        bool IsConnected { get; }
        public void Connect();
        public void Disconnect();
    }
}
