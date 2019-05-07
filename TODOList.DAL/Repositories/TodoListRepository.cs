using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.DAL.EF;
using TODOList.DAL.Entities;

namespace TODOList.DAL.Repositories
{
    public class TodoListRepository : IRepository<TodoList>
    {
        private TODOContext db;

        public TodoListRepository(TODOContext context)
        {
            this.db = context;
        }

        public IEnumerable<TodoList> GetAll()
        {
            return db.TodoLists;
        }

        public TodoList Get(int id)
        {
            return db.TodoLists.Find(id);
        }

        public void Create(TodoList list)
        {
            db.TodoLists.Add(list);
        }

        public void Update(TodoList list)
        {
            db.Entry(list).State = EntityState.Modified;
        }

        public IEnumerable<TodoList> Find(Func<TodoList, Boolean> predicate)
        {
            return db.TodoLists.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            TodoList list = db.TodoLists.Find(id);
            if (list != null)
            {
                db.TodoLists.Remove(list);
            }
        }
    }
}
