using AutoMapper;
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
    public class TaskService : ITaskService
    {
        public TaskService()
        {
            Context = new Context();
        }

        Context Context { get; set; }

        public void MakeTask(TaskDTO taskDTO)
        {
            if (taskDTO == null)
                throw new Exception("Task equals null");
            Task task = new Task
            {
                TodoListId = taskDTO.TodoListId,
                DateCompletion = taskDTO.DateCompletion,
                DateReminder = taskDTO.DateReminder
            };
            Context.Tasks.Add(task);
            Context.SaveChanges();
        }

        public TaskDTO GetTask(int? id)
        {
            if (id == null)
                throw new Exception("id of Task is null");
            var task = Context.Tasks.Find(id.Value);
            if (task == null)
                throw new Exception("The task wasn't found");
            return new TaskDTO { Name = task.Name, DateCompletion = task.DateCompletion, DateReminder = task.DateReminder };
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, List<TaskDTO>>(Context.Tasks.ToList());
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            if (taskDTO == null)
                throw new Exception("Task == null");
            Task task = new Task
            {
                Id = taskDTO.Id,
                TodoListId = taskDTO.TodoListId,
                Name = taskDTO.Name,
                DateCompletion = taskDTO.DateCompletion,
                DateReminder = taskDTO.DateReminder
            };
            Context.Tasks.Update(task);
            Context.SaveChanges();
        }

        public void DelTask(int? id)
        {
            if (id == null)
                throw new Exception("Task id is null");
            var task = Context.Tasks.Find(id);
            Context.Tasks.Remove(task);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
