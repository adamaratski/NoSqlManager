using NoSqlManager.Infrastructure.Contracts;
using Prism.Events;
using System;

namespace NoSqlManager.Infrastructure.Events
{
    public class ConnectedEvent : PubSubEvent<IConnection>
    {
    }
}
