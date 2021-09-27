using Prism.Events;
using Prism.Ioc;
using NoSqlManager.Infrastructure.Contracts;
using NoSqlManager.Infrastructure.Events;
using NoSqlManager.Infrastructure.Services;
using System;
using System.Collections.Concurrent;

namespace NoSqlManager.Infrastructure.Models
{
    public class ConnectionManager
    {
        private IContainerProvider _container;
        private IEventAggregator _eventAggregator;
        private DataService _dataService;

        protected ConcurrentDictionary<Guid, IConnection> Connections = new ConcurrentDictionary<Guid, IConnection>();
        protected ConcurrentDictionary<Guid, IClient> Clients = new ConcurrentDictionary<Guid, IClient>();

        public ConnectionManager(IContainerProvider container)
        {
            this._container = container;
            this._eventAggregator = container.Resolve<IEventAggregator>();
            this._dataService = container.Resolve<DataService>();

            

            this._eventAggregator.GetEvent<ApplicationInitializedEvent>().Subscribe(() =>
            {
                var connections = this._dataService.GetRedisConnections();
                foreach (var connection in connections)
                {
                    this.Connections.TryAdd(connection.Id, connection);
                }

                this._eventAggregator.GetEvent<RefreshConnectionsEvent>().Publish(connections);
            });

            this._eventAggregator.GetEvent<ApplicationExitEvent>().Subscribe(() =>
            {
                foreach (var key in this.Connections.Keys)
                {

                    this.Connections.TryGetValue(key, out IConnection connection);
                    if (connection.IsConnected)
                    {
                        connection.Disconnect();
                    }
                }

                this._dataService.SaveRedisConnections(this.Connections.Values);
            });
        }

        public void AddConnection(IConnection connection)
        {
            if (this.Connections.TryAdd(connection.Id, connection))
            {
                this._eventAggregator.GetEvent<AddedConnectionEvent>().Publish(connection);
            }

            this._dataService.SaveRedisConnections(this.Connections.Values);
        }

        public void DeleteConnection(IConnection connection)
        {
            if (this.Connections.TryRemove(connection.Id, out IConnection removedConnection))
            {
                this._eventAggregator.GetEvent<RemovedConnectionEvent>().Publish(removedConnection);
            }

            this._dataService.SaveRedisConnections(this.Connections.Values);
        }
    }
}
