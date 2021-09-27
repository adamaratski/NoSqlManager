using Prism.Events;
using NoSqlManager.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Events
{
    public class RefreshConnectionsEvent : PubSubEvent<IEnumerable<IConnection>>
    {
    }
}
