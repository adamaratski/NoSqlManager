using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using NoSqlManager.Contracts;
using NoSqlManager.Infrastructure.Events;
using NoSqlManager.Infrastructure.Models;
using NoSqlManager.Infrastructure.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using NoSqlManager.Infrastructure.Contracts;

namespace NoSqlManager.ViewModels
{
    public class ConnectionsViewModel : BindableBase
    {
        private IDialogService _dialogService;
        private IContainerProvider _containerProvider;
        private IEventAggregator _eventAggregator;
        private DataService _dataService;
        private IMapper _mapper;
        private IConnectionViewModel _selectedConnection;
        private ConnectionManager _connectionManager;

        public IConnectionViewModel SelectedConnection
        {
            get { return this._selectedConnection; }
            set
            {
                this.SetProperty(ref this._selectedConnection, value, nameof(this.SelectedConnection));
            }
        }
        public ObservableCollection<IConnectionViewModel> Connections { get; }

        public ConnectionsViewModel(IContainerProvider conteinerProvider, IMapper mapper, IDialogService dialogService, IEventAggregator eventAggregator, DataService dataService, ConnectionManager connectionManager)
        {
            this._dialogService = dialogService;
            this._dataService = dataService;
            this._containerProvider = conteinerProvider;
            this._eventAggregator = eventAggregator;
            this._mapper = mapper;
            this._connectionManager = connectionManager;

            this.Connections = new ObservableCollection<IConnectionViewModel>();

            this._eventAggregator.GetEvent<RefreshConnectionsEvent>().Subscribe((connections) =>
            {
                this.Connections.Clear();
                this.Connections.AddRange(connections.Select(item =>
                {
                    switch (item.Type)
                    {
                        case ConnectionType.Redis:
                            return new RedisConnectionViewModel(item as RedisConnection);
                        default:
                            return null;
                    }
                }).Where(item => item != null));
            });

            this._eventAggregator.GetEvent<AddedConnectionEvent>().Subscribe(connection =>
            {
                switch (connection.Type)
                {
                    case ConnectionType.Redis:
                        this.Connections.Add(new RedisConnectionViewModel(connection as RedisConnection));
                        break;
                    default:
                        break;
                }
            });

            this._eventAggregator.GetEvent<RemovedConnectionEvent>().Subscribe(connection =>
            {
                switch (connection.Type)
                {
                    case ConnectionType.Redis:
                        var items = this.Connections.Where(item => item.Id == connection.Id).ToList();
                        items.ForEach(item => this.Connections.Remove(item));
                        break;
                    default:
                        break;
                }
            });
        }

        public Helpers.DelegateCommand<ConnectionType?> AddConnectionCommand => new Helpers.DelegateCommand<ConnectionType?>((type) => this.AddConnectionDialog(type ?? ConnectionType.Unknown));
        public Helpers.DelegateCommand<IConnectionViewModel> EditConnectionCommand => new Helpers.DelegateCommand<IConnectionViewModel>((connection) => this.EditConnectionDialog(connection));
        public Helpers.DelegateCommand<IConnectionViewModel> DeleteConnectionCommand => new Helpers.DelegateCommand<IConnectionViewModel>((connection) => this.DeleteConnection(connection), connection => connection != null);
        public Helpers.DelegateCommand<IConnectionViewModel> ConnectCommand => new Helpers.DelegateCommand<IConnectionViewModel>((connection) =>
        {
            connection.Connect();
        }, connection => connection != null);
        public Helpers.DelegateCommand<IConnectionViewModel> DisconnectCommand => new Helpers.DelegateCommand<IConnectionViewModel>((connection) =>
        {
            connection.Disconnect();
        }, connection => connection != null);

        private void DeleteConnection(IConnectionViewModel connection)
        {
            if (connection.IsConnected)
            {
                connection.Disconnect();
            }

            this._connectionManager.DeleteConnection(connection.GetSource());
        }

        private void EditConnectionDialog(IConnectionViewModel connection)
        {
            switch (connection.Type)
            {
                case ConnectionType.Redis:
                    this._dialogService.ShowDialog("RedisConnectionDialog", connection.GetSource(), result =>
                    {
                        switch (result.Result)
                        {
                            case ButtonResult.OK:
                                this._mapper.Map(result.Parameters, (RedisConnectionViewModel)connection);
                                this._dataService.SaveRedisConnections(this.Connections.Select(item => item.GetSource()));
                                break;
                            case ButtonResult.Cancel:
                                break;
                        }
                    });
                    break;
            }
        }

        private void AddConnectionDialog(ConnectionType type)
        {
            switch (type)
            {
                case ConnectionType.Redis:
                    this._dialogService.ShowDialog("RedisConnectionDialog", result =>
                    {
                        switch (result.Result)
                        {
                            case ButtonResult.OK:
                                var connection = this._containerProvider.Resolve<IConnection>(ConnectionType.Redis.ToString());
                                this._mapper.Map(result.Parameters, connection);
                                this._connectionManager.AddConnection(connection);
                                break;
                            case ButtonResult.Cancel:
                                break;
                        }
                    });
                    break;
            }
        }
    }
}
