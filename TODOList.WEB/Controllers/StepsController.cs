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
    public class StepsController : Controller
    {
        private readonly StepService stepService;

        public StepsController(StepService stepService)
        {
            this.stepService = stepService;
        }

        [HttpGet]
        public IEnumerable<StepDTO> Get()
        {
            var lists = stepService.GetAll();
            return lists;
        }

        [HttpGet("{id}")]
        public async Task<StepDTO> Get(int id)
        {
            var step = await this.stepService.Get(id);
            return step;
        }

        [HttpPost]
        public async Task<StepDTO> Add([FromBody] StepDTO step)
        {
            var res = await stepService.Create(step);
            return res;
        }

        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] StepDTO step)
        {
            int res = await stepService.Update(id, step);
        }

        [HttpDelete("{id}")]
        public async Task<StepDTO> Delete(int id)
        {
            return await stepService.Delete(id);
        }
    }
}