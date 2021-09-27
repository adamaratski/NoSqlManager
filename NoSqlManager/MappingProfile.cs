using AutoMapper;
using Prism.Services.Dialogs;
using NoSqlManager.Infrastructure.Models;
using NoSqlManager.ViewModels;
using System;

namespace NoSqlManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AddDialogParametersMappers();
        }

        private void AddDialogParametersMappers()
        {
            this.CreateMap<RedisConnection, RedisConnection>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            this.CreateMap<IDialogParameters, RedisConnection>()
                 .ForMember(x => x.Id, opt => opt.MapFrom(src => src.GetValue<Guid>("Id")))
                 .ForMember(x => x.Name, opt => opt.MapFrom(src => src.GetValue<string>("Name")))
                 .ForMember(x => x.Host, opt => opt.MapFrom(src => src.GetValue<string>("Host")))
                 .ForMember(x => x.Password, opt => opt.MapFrom(src => src.GetValue<string>("Password")))
                 .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.GetValue<string>("UserName")))
                 .ForMember(x => x.Port, opt => opt.MapFrom(src => src.GetValue<int>("Port")))
                 .ForMember(x => x.UseSsl, opt => opt.MapFrom(src => src.GetValue<bool>("UseSsl")));

            this.CreateMap<RedisConnection, DialogParameters>()
                 .ConstructUsing(src => new DialogParameters() {
                     { "Id", src.Id},
                     { "Name", src.Name },
                     { "Host", src.Host },
                     { "Password", src.Password },
                     { "UserName", src.UserName },
                     { "Port", src.Port },
                     { "UseSsl", src.UseSsl },
                 } );

            this.CreateMap<IDialogParameters, RedisConnectionViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.GetValue<Guid>("Id")))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.GetValue<string>("Name")))
                .ForMember(x => x.Host, opt => opt.MapFrom(src => src.GetValue<string>("Host")))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.GetValue<string>("Password")))
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.GetValue<string>("UserName")))
                .ForMember(x => x.Port, opt => opt.MapFrom(src => src.GetValue<int>("Port")))
                .ForMember(x => x.UseSsl, opt => opt.MapFrom(src => src.GetValue<bool>("UseSsl")));

            this.CreateMap<RedisConnectionViewModel, DialogParameters>()
                .ConstructUsing(src => new DialogParameters() {
                     { "Id", src.Id},
                     { "Name", src.Name },
                     { "Host", src.Host },
                     { "Password", src.Password },
                     { "UserName", src.UserName },
                     { "Port", src.Port },
                     { "UseSsl", src.UseSsl },
                });

            this.CreateMap<RedisConnectionInfo, RedisConnection>();
        }
    }
}
