using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.DAL.Entities;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.DAL.EF
{
    public class TODOContext : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; }

        public TODOContext(DbContextOptions<TODOContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
