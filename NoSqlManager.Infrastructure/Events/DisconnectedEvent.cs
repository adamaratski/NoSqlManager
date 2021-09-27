using Prism.Events;
using System;

namespace NoSqlManager.Infrastructure.Events
{
    public class DisconnectedEvent : PubSubEvent<Guid>
    {
    }
}
