using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.DAL.EF;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Task = TODOList.DAL.Entities.Task;

namespace TODOList.BLL.Services
{
    public class TaskService : ITaskService
    {
        IUnitOfWork Database { get; set; }

        public TaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

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
            Database.Tasks.Create(task);
            Database.Save();
        }

        public TaskDTO GetTask(int? id)
        {
            if (id == null)
                throw new Exception("id of Task is null");
            var task = Database.Tasks.Get(id.Value);
            if (task == null)
                throw new Exception("The task wasn't found");
            return new TaskDTO { Name = task.Name, DateCompletion = task.DateCompletion, DateReminder = task.DateReminder };
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, List<TaskDTO>>(Database.Tasks.GetAll());
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
            Database.Tasks.Update(task);
        }

        public void DelTask(int? id)
        {
            if (id == null)
                throw new Exception("Task id is null");
            Database.Tasks.Delete(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
