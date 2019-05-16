using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.DAL;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.BLL.Services
{
    public class TaskService : IService<TaskDTO>
    {
        Context Context { get; set; }

        public TaskService()
        {
            Context = new Context();
        }

        async public Task<TaskDTO> Create(TaskDTO taskDTO)
        {
            taskDTO.DateStart = taskDTO.DateStart.AddHours(3);
            taskDTO.DateEnd = taskDTO.DateEnd.AddHours(3);
            var task = await Context.Tasks.AddAsync(new Task { TodoListId = taskDTO.TodoListId, Name = taskDTO.Name, DateStart = taskDTO.DateStart, DateEnd = taskDTO.DateEnd });
            var res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<Task, TaskDTO>(task.Entity) : null;
        }

        async public Task<TaskDTO> Get(int id)
        {
            var task = await Context.Tasks.FindAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return task != null ? mapper.Map<Task, TaskDTO>(task) : null;
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, List<TaskDTO>>(Context.Tasks.ToList());
        }

        public IEnumerable<TaskDTO> GetAll(int todoId)
        {
            var tasks = Context.Tasks.Where(a => a.TodoListId == todoId).ToList();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, List<TaskDTO>>(tasks.ToList());
        }

        async public Task<int> Update(int id, TaskDTO taskDTO)
        {
            var task = await Context.Tasks.FindAsync(id);
            task.TodoListId = taskDTO.TodoListId;
            task.Name = taskDTO.Name;
            task.DateStart = taskDTO.DateStart;
            task.DateEnd = taskDTO.DateEnd;
            return await Context.SaveChangesAsync();
        }

        async public Task<TaskDTO> Delete(int id)
        {
            var task = await Context.Tasks.FindAsync(id);
            if (task == null)
                return null;
            Context.Tasks.Remove(task);
            int res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<Task, TaskDTO>(task) : null;
        }

    }
}
