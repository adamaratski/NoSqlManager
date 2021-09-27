using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace NoSqlManager.ViewModels
{
    public class RedisConnectionDialogViewModel : BindableBase, IDialogAware
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool UseSsl { get; set; }

        public DelegateCommand<string> CloseDialogCommand => new DelegateCommand<string>(CloseDialog);

        public RedisConnectionDialogViewModel()
        {

        }
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
            }
            else
            {
                if (parameter?.ToLower() == "false")
                { 
                    result = ButtonResult.Cancel; 
                }
            }

            var parameters = new DialogParameters();
            parameters.Add("Id", this.Id);
            parameters.Add("Name", this.Name);
            parameters.Add("Host", this.Host);
            parameters.Add("Port", this.Port);
            parameters.Add("Password", this.Password);
            parameters.Add("UserName", this.UserName);
            parameters.Add("UseSsl", this.UseSsl);

            RaiseRequestClose(new DialogResult(result, parameters));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        #region IDialogAware
        public string Title => "Redis connection details";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.Name = parameters.GetValue<string>("Name");
            this.Host = parameters.GetValue<string>("Host");
            this.Port = parameters.GetValue<int>("Port");
            this.Password = parameters.GetValue<string>("Password");
            this.UserName = parameters.GetValue<string>("UserName");
            this.UseSsl = parameters.GetValue<bool>("UseSsl");

            this.RaisePropertyChanged(nameof(this.Name));
            this.RaisePropertyChanged(nameof(this.Host));
            this.RaisePropertyChanged(nameof(this.Port));
            this.RaisePropertyChanged(nameof(this.Password));
            this.RaisePropertyChanged(nameof(this.UserName));
            this.RaisePropertyChanged(nameof(this.UseSsl));
        }
        #endregion
    }
}
