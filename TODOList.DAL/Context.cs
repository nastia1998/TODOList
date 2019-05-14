using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using TODOList.DAL.Entities;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.DAL
{
    public class Context : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AppUser> Users { get; set; }

        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                //.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
