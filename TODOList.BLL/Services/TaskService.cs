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
            throw new NotImplementedException();
        }

        public TaskDTO GetTask(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, List<TaskDTO>>(Database.Tasks.GetAll());
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            throw new NotImplementedException();
        }

        public void DelTask(int? id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
