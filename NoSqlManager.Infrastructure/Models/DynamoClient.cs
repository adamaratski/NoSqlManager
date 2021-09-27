using NoSqlManager.Infrastructure.Contracts;
using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Models
{
    public class DynamoClient : IClient
    {
        public async Task<bool> Connect()
        {
            throw new System.NotImplementedException();
        }

        public void Disconnect()
        {
            throw new System.NotImplementedException();
        }

        public string GetLogs()
        {
            throw new System.NotImplementedException();
        }
    }
}
