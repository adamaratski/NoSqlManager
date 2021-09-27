using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using NoSqlManager.Infrastructure.Models;
using StackExchange.Redis;

namespace NoSqlManager.Providers.StackExchange
{
    public class RedisProvider : NoSqlManager.Infrastructure.Contracts.IRedisProvider
    {
        private IMapper _mapper;
        public bool IsConnected => this.Redis?.IsConnected ?? false;
        protected ConnectionMultiplexer Redis { get; private set; }
        private StringWriter _logWriter;

        public event EventHandler<FailureEventArgs> Failure;
        

        public RedisProvider(IMapper mapper)
        {
            this._mapper = mapper;
            this._logWriter = new StringWriter();
        }
        public async Task<bool> Connect(RedisConnection connection)
        {
            var options = this._mapper.Map<ConfigurationOptions>(connection);
            this.Redis = await ConnectionMultiplexer.ConnectAsync(options, this._logWriter);
            var logs = this._logWriter.GetStringBuilder().ToString();
            return this.IsConnected;
        }

        public async Task Disconnect()
        {
            await this.Redis?.CloseAsync();
        }

        protected void OnFailure(string message)
        {
            if (this.Failure != null)
            {
                this.Failure(this, new FailureEventArgs(message));
            }
        }

        public string GetLogs()
        {
            return this._logWriter.GetStringBuilder().ToString();
        }
    }
}
