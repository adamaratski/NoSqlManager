using NoSqlManager.Infrastructure.Models;
using System;

namespace NoSqlManager.Infrastructure.Contracts
{
    public interface IConnection
    {
        Guid Id { get; }
        string Name { get; set; }
        ConnectionType Type { get; }
        bool IsConnected { get; }
        void Connect();
        void Disconnect();
        IClient Client { get; }
    }
}
