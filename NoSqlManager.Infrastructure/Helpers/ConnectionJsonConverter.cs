using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoSqlManager.Infrastructure.Contracts;
using NoSqlManager.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlManager.Infrastructure.Helpers
{
    public class ConnectionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;// System.Attribute.GetCustomAttributes(objectType).Any(v => v is IConnection);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray items = JArray.Load(reader);
            var result = new List<BaseConnectionInfo>();

            foreach(var item in items)
            {
                switch (item["Type"].ToObject<ConnectionType>())
                {
                    case ConnectionType.Redis:
                        result.Add(item.ToObject<RedisConnectionInfo>());

                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
