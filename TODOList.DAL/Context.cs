using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TODOList.DAL.Entities;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.DAL
{
    public class Context : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<User> Users { get; set; }

        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
