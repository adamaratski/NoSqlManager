using AutoMapper;
using NoSqlManager.Infrastructure.Contracts;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace NoSqlManager.ViewModels
{
    public class DatabaseExplorerViewModel : BindableBase
    {
        private IMapper _mapper;
        private IContainerProvider _containerProvider;
        private IEventAggregator _eventAggregator;

        protected ObservableCollection<BaseClientViewModel<IClient>> Clients { get; }

        public DatabaseExplorerViewModel(IContainerProvider conteinerProvider, IMapper mapper, IEventAggregator eventAggregator)
        {
            this._mapper = mapper;
            this._containerProvider = conteinerProvider;
            this._eventAggregator = eventAggregator;

            this.Clients = new ObservableCollection<BaseClientViewModel<IClient>>();
        }
    }
}
