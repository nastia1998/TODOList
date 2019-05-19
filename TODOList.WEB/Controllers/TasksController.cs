using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOList.BLL.DTO;
using TODOList.BLL.Services;

namespace TODOList.WEB.Controllers
{
    [Route("api/todolists/{todoId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService taskService;

        public TasksController(TaskService taskService)
        {
            this.taskService = taskService;
        }

        //[HttpGet]
        //public IEnumerable<TaskDTO> Get()
        //{
        //    var tasks = taskService.GetAll();
        //    return tasks;
        //}

        [HttpGet("{id}")]
        public async Task<TaskDTO> Get(int id)
        {
            var task = await this.taskService.Get(id);
            return task;
        }

        [HttpGet]
        public IEnumerable<TaskDTO> GetByTodoId(int todoId)
        {
            var tasks = this.taskService.GetAll(todoId);
            return tasks;
        }

        [HttpPost]
        public async Task<TaskDTO> Add([FromBody] TaskDTO task)
        {
            var res = await taskService.Create(task);
            return res;
        }

        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] TaskDTO task)
        {
            int res = await taskService.Update(id, task);
        }

        [HttpDelete("{id}")]
        public async Task<TaskDTO> Delete(int id)
        {
            return await taskService.Delete(id);
        }
    }
}