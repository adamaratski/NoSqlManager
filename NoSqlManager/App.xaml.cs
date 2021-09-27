using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using NoSqlManager.Infrastructure.Configuration;
using NoSqlManager.Infrastructure.Contracts;
using NoSqlManager.Infrastructure.Events;
using NoSqlManager.Infrastructure.Models;
using NoSqlManager.Infrastructure.Services;
using NoSqlManager.ViewModels;
using NoSqlManager.Views;
using System.IO;
using System.Windows;
using DryIoc;
using Prism.DryIoc;
using NoSqlManager.Providers;

namespace NoSqlManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <inheritdoc />
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IConfiguration>(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build());

            // AutoMapper
            containerRegistry.RegisterSingleton<IMapper>(() =>
            {
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                    mc.AddProfile(new ProvidersProfile());

                });
                return mapperConfig.CreateMapper();
            });
            
            containerRegistry.Register<DataService>();
            containerRegistry.Register<IRedisProvider, NoSqlManager.Providers.StackExchange.RedisProvider>();
            containerRegistry.RegisterDialog<RedisConnectionDialog, RedisConnectionDialogViewModel>();
            containerRegistry.Register<IConnection, RedisConnection>(ConnectionType.Redis.ToString());
            containerRegistry.RegisterSingleton<ConnectionManager>(() => new ConnectionManager(this.Container));
        }

        /// <inheritdoc />
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.Container.Resolve<IEventAggregator>().GetEvent<ApplicationInitializedEvent>().Publish();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            this.Container.Resolve<IEventAggregator>().GetEvent<ApplicationExitEvent>().Publish();
        }
    }
}
