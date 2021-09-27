using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using NoSqlManager.Infrastructure.Events;
using NoSqlManager.Models;
using NoSqlManager.Views;
using System.Windows;
using System.Windows.Input;

namespace NoSqlManager.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        //[Import]
        public IRegionManager RegionManager { get; }

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.RegionManager = regionManager;
            this.RegionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(ConnectionsView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.WorkspaceRegion, typeof(DatabaseExplorerView));
            eventAggregator.GetEvent<ApplicationInitializedEvent>().Subscribe(() => 
            {
                this.LoadData();
            });
        }

        public void LoadData()
        {
            var client = StackExchange.Redis.ConnectionMultiplexer.Connect("PROD-IVR.redis.cache.windows.net:6380,password=WOoVGpt1aU5Jev2txJ0lmQKBg3M8YlLpP69oWL+xGBo=,ssl=true,sslprotocols=Tls11|Tls12");
            var counters = client.GetCounters();
            var status = client.GetStatus();
            var subscriber = client.GetSubscriber();
            var database = client.GetDatabase();
            var result = database.Execute("INFO");
        }

        public ICommand ExitCommand { get; private set; }

        private void OnExited()
        {
            Application.Current.Shutdown();
        }
    }
}
