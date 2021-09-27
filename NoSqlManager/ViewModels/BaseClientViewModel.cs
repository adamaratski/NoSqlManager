using NoSqlManager.Infrastructure.Contracts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlManager.ViewModels
{
    public class BaseClientViewModel<T> : BindableBase where T : IClient
    {
    }
}
