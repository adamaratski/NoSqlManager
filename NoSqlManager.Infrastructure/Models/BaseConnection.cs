using NoSqlManager.Infrastructure.Contracts;
using Prism.Ioc;
using System;

namespace NoSqlManager.Infrastructure.Models
{
    public abstract class BaseConnection : IConnection
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public abstract ConnectionType Type { get; }
        protected IContainerProvider ContainerProvider { get; }

        public abstract bool IsConnected { get; }

        public IClient Client { get; protected set; }

        public BaseConnection(IContainerProvider containerProvider)
        {
            this.Id = Guid.NewGuid();
            this.ContainerProvider = containerProvider;
        }

        public void Connect()
        {
            this.Client.Connect();
        }

        public void Disconnect()
        {
            this.Client.Disconnect();
        }
    }
}
