using NoSqlManager.Infrastructure.Contracts;
using Prism.Events;

namespace NoSqlManager.Infrastructure.Events
{
    public class AddedConnectionEvent : PubSubEvent<IConnection>
    {
    }
}
