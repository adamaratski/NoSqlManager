using NoSqlManager.Infrastructure.Models;
using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Contracts
{
    public interface IRedisProvider
    {
        Task<bool> Connect(RedisConnection connection);
        Task Disconnect();
        string GetLogs();
    }
}
