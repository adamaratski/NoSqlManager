using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoSqlManager.Infrastructure.Configuration;
using NoSqlManager.Infrastructure.Contracts;
using NoSqlManager.Infrastructure.Helpers;
using NoSqlManager.Infrastructure.Models;
using Prism.Ioc;
using System.Collections.Generic;
using System.IO;

namespace NoSqlManager.Infrastructure.Services
{
    public class DataService
    {
        private IConfiguration _configuration;
        private IContainerProvider _containerProvider;
        private IMapper _mapper;

        public DataService(IMapper mapper, IConfiguration configuration, IContainerProvider containerProvider)
        {
            this._mapper = mapper;
            this._configuration = configuration;
            this._containerProvider = containerProvider;
        }

        public IList<IConnection> GetRedisConnections()
        {
            var settings = _configuration.GetSection("AppSettings").Get<AppSettings>();
            var connectionsSettings = File.Exists(settings.ConnectionsFile) ? JsonConvert.DeserializeObject<IList<BaseConnectionInfo>>(File.ReadAllText(settings.ConnectionsFile), new ConnectionJsonConverter()) : new List<BaseConnectionInfo>();

            List<IConnection> connections = new List<IConnection>();
            foreach (var connectionSettings in connectionsSettings)
            {
                var connection = this._containerProvider.Resolve<IConnection>(connectionSettings.Type.ToString());
                this._mapper.Map(connectionSettings, connection);
                connections.Add(connection);
            }

            return connections;
        }

        public bool SaveRedisConnections(IEnumerable<IConnection> connections)
        {
            try
            {
                var settings = _configuration.GetSection("AppSettings").Get<AppSettings>();
                if (File.Exists(settings.ConnectionsFile))
                {
                    File.Delete(settings.ConnectionsFile);
                }

                File.WriteAllText(settings.ConnectionsFile, JsonConvert.SerializeObject(connections));

                return true;
            }
            catch { return false; }
        }
    }
}
