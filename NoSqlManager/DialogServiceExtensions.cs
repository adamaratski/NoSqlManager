using AutoMapper;
using Prism.Services.Dialogs;
using System;
using Prism.Ioc;

namespace NoSqlManager
{
    public static class DialogServiceExtensions
    {
        public static void ShowDialog<T>(this IDialogService dialogService, string name, T instnce, Action<IDialogResult> callBack)
        {
            var mapper = (Prism.DryIoc.PrismApplication.Current as Prism.DryIoc.PrismApplication).Container.Resolve<IMapper>();
            dialogService.ShowDialog(name, mapper.Map<DialogParameters>(instnce), callBack);
        }
    }
}
