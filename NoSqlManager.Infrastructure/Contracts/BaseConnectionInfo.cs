using NoSqlManager.Infrastructure.Models;
using System;

namespace NoSqlManager.Infrastructure.Contracts
{
    public abstract class BaseConnectionInfo
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public abstract ConnectionType Type { get; }
    }
}
