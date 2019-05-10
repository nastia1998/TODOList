using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.DAL;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Services
{
    public class StepService : IService<StepDTO>
    {

        Context Context { get; set; }

        public StepService()
        {
            Context = new Context();
        }

        async public Task<StepDTO> Create(StepDTO stepDTO)
        {
            var step = await Context.Steps.AddAsync(new Step { TaskId = stepDTO.TaskId, Name = stepDTO.Name, IsDone = stepDTO.IsDone });
            var res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Step, StepDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<Step, StepDTO>(step.Entity) : null;
        }

        async public Task<StepDTO> Get(int id)
        {
            var step = await Context.Steps.FindAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Step, StepDTO>()).CreateMapper();
            return step != null ? mapper.Map<Step, StepDTO>(step) : null;
        }

        public IEnumerable<StepDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Step, StepDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Step>, List<StepDTO>>(Context.Steps.ToList());
        }

        async public Task<int> Update(int id, StepDTO stepDTO)
        {
            var step = await Context.Steps.FindAsync(id);
            step.Name = stepDTO.Name;
            step.IsDone = stepDTO.IsDone;
            return await Context.SaveChangesAsync();
        }

        async public Task<StepDTO> Delete(int id)
        {
            var step = await Context.Steps.FindAsync(id);
            if (step == null)
                return null;
            Context.Steps.Remove(step);
            int res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Step, StepDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<Step, StepDTO>(step) : null;
        }
    }
}
