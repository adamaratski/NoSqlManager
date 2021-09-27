using NoSqlManager.Infrastructure.Contracts;
using System;

namespace NoSqlManager.Infrastructure.Models
{
    public class RedisConnectionInfo : BaseConnectionInfo
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool UseSSL { get; set; }
        public override ConnectionType Type => ConnectionType.Redis;
    }
}
