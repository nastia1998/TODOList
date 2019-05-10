using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODOList.BLL.DTO;
using TODOList.BLL.Services;

namespace TODOList.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService taskService;

        public TasksController(TaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public IEnumerable<TaskDTO> Get()
        {
            var lists = taskService.GetAll();
            return lists;
        }

        [HttpGet("{id}")]
        public async Task<TaskDTO> Get(int id)
        {
            var task = await this.taskService.Get(id);
            return task;
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