using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.DAL.EF;
using TODOList.DAL.Entities;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TODOContext db;
        private TodoListRepository listRepository;
        private TaskRepository taskRepository;
        private StepRepository stepRepository;

        //public EFUnitOfWork()
        //{
        //    db = new TODOContext();
        //}

        public IRepository<TodoList> TodoLists
        {
            get
            {
                if(listRepository == null)
                {
                    listRepository = new TodoListRepository(db);
                }
                return listRepository;
            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new TaskRepository(db);
                }
                return taskRepository;
            }
        }

        public IRepository<Step> Steps
        {
            get
            {
                if (stepRepository == null)
                {
                    stepRepository = new StepRepository(db);
                }
                return stepRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
