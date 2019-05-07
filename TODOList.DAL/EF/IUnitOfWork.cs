using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.DAL.Entities;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.DAL.EF
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TodoList> TodoLists { get; }
        IRepository<Task> Tasks { get; }
        IRepository<Step> Steps { get; }
        void Save();
    }
}
