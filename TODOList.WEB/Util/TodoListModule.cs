using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOList.BLL.Interfaces;
using TODOList.BLL.Services;

namespace TODOList.WEB.Util
{
    public class TodoListModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITodoListService>().To<TodoListService>();
        }
    }
}
