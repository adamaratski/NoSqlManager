using NoSqlManager.Base;
using NoSqlManager.Infrastructure.Models;
using System;

namespace NoSqlManager.ViewModels
{
    public class RedisConnectionViewModel : BaseConnectionViewModel<RedisConnection>
    {
        public string Name
        {
            get => this.Source.Name;
            set
            {
                this.Source.Name = value;
                this.RaisePropertyChanged(nameof(this.Name));
            }
        }

        public string Host
        {
            get => this.Source.Host;
            set
            {
                this.Source.Host = value;
                this.RaisePropertyChanged(nameof(this.Host));
            }
        }

        public string Password
        {
            get => this.Source.Password;
            set
            {
                this.Source.Password = value;
                this.RaisePropertyChanged(nameof(this.Password));
            }
        }

        public string UserName
        {
            get => this.Source.UserName;
            set
            {
                this.Source.UserName = value;
                this.RaisePropertyChanged(nameof(this.UserName));
            }
        }

        public int Port
        {
            get => this.Source.Port;
            set
            {
                this.Source.Port = value;
                this.RaisePropertyChanged(nameof(this.Port));
            }
        }
        public bool UseSsl
        {
            get => this.Source.UseSsl;
            set
            {
                this.Source.UseSsl = value;
                this.RaisePropertyChanged(nameof(this.UseSsl));
            }
        }

        public bool IsConnected { get { return true; } }

        public override ConnectionType Type => ConnectionType.Redis;

        public RedisConnectionViewModel(RedisConnection connection)
            :base(connection)
        {

        }

        public override void Connect()
        {
            this.Source.Connect();
        }

        public override void Disconnect()
        {
            this.Source.Disconnect();
        }
    }
}
