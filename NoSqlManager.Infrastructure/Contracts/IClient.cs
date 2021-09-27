using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Contracts
{
    public interface IClient
    {
        //IConnection Connection { get; }

        Task<bool> Connect();
        void Disconnect();
        string GetLogs();
    }
}
