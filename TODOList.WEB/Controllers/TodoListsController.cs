using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOList.BLL.DTO;
using TODOList.BLL.Services;

namespace TODOList.WEB.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoListService listService;

        public TodoListsController(TodoListService listService)
        {
            this.listService = listService;
        }

        //[HttpGet]
        //public IEnumerable<TodoListDTO> Get()
        //{
        //    var lists = listService.GetAll();
        //    return lists;
        //}

        [HttpGet("{id}")]
        public async Task<TodoListDTO> Get(int id)
        {
            var list = await this.listService.Get(id);
            return list;
        }

        [HttpGet]
        public IEnumerable<TodoListDTO> GetByUserId(int userId)
        {
            var lists = this.listService.GetAll(userId);
            return lists;
        }

        [HttpPost]
        public async Task<TodoListDTO> Add([FromBody] TodoListDTO list)
        {
            var res = await listService.Create(list);
            return res;
        }

        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] TodoListDTO list)
        {
            int res = await listService.Update(id, list);
        }

        [HttpDelete("{id}")]
        public async Task<TodoListDTO> Delete(int id)
        {
            return await listService.Delete(id);
        }
    }
}