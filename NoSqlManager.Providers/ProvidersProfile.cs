using AutoMapper;
using NoSqlManager.Infrastructure.Models;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Net;

namespace NoSqlManager.Providers
{
    public class ProvidersProfile : Profile
    {
        public ProvidersProfile()
        {
            this.CreateMap<RedisConnection, ConfigurationOptions>()
                .ForMember(x => x.EndPoints, opt => opt.MapFrom(src => new EndPointCollection(src.EndPoints)))
                .ForMember(x => x.Ssl, opt => opt.MapFrom(src => src.UseSsl));
        }
    }
}
