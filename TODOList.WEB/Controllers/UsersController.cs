using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOList.BLL.DTO;
using TODOList.BLL.Services;

namespace TODOList.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            var users = userService.GetAll();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
            var user = await this.userService.Get(id);
            return user;
        }

        [HttpPost]
        public async Task<UserDTO> SignUp([FromBody] UserDTO user)
        {
            var res = await userService.Create(user);
            return res;
        }

        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] UserDTO user)
        {
            int res = await userService.Update(id, user);
        }

        [HttpDelete("{id}")]
        public async Task<UserDTO> Delete(int id)
        {
            return await userService.Delete(id);
        }
    }
}