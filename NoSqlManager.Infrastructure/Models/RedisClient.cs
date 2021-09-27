using NoSqlManager.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Models
{
    public class RedisClient : IClient 
    {
        private IRedisProvider _provider;
        private RedisConnection _connection;
        public RedisClient(RedisConnection connection, IRedisProvider provider)
        {
            this._provider = provider;
            this._connection = connection;
        }

        public async Task<bool> Connect()
        {
            return await this._provider.Connect(this._connection);
        }

        public void Disconnect()
        {
            this._provider.Connect(this._connection);
        }

        public string GetLogs()
        {
            return this._provider.GetLogs();
        }
    }
}
