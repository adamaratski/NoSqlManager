using NoSqlManager.Infrastructure.Contracts;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Models
{
    public class RedisConnection : BaseConnection
    {
        public string Host { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public override ConnectionType Type => ConnectionType.Redis;
        public override bool IsConnected => true;
        public IList<EndPoint> EndPoints => new List<EndPoint>() { new DnsEndPoint(this.Host, this.Port)};

        public RedisConnection(IContainerProvider containerProvider)
            :base(containerProvider)
        {
            this.Client = new RedisClient(this, containerProvider.Resolve<IRedisProvider>());
        }
    }
}
