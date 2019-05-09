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
    public class StepService : IStepService
    {
        public StepService()
        {
            Context = new Context();
        }

        Context Context { get; set; }

        public void MakeStep(StepDTO stepDTO)
        {
            if (stepDTO == null)
                throw new Exception("Step == null");
            Step step = new Step
            {
                TaskId = stepDTO.TaskId,
                Name = stepDTO.Name,
                IsDone = stepDTO.IsDone
            };
            Context.Steps.Add(step);
            Context.SaveChanges();
        }

        public StepDTO GetStep(int? id)
        {
            if (id == null)
                throw new Exception("Id of step is null");
            var step = Context.Steps.Find(id.Value);
            if (step == null)
                throw new Exception("Step wasn't found");
            return new StepDTO { Name = step.Name, IsDone = step.IsDone };
        }

        public IEnumerable<StepDTO> GetSteps()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Step, StepDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Step>, List<StepDTO>>(Context.Steps.ToList());
        }

        public void UpdateStep(StepDTO stepDTO)
        {
            if (stepDTO == null)
                throw new Exception("Step == null");
            Step step = new Step
            {
                Id = stepDTO.Id,
                Name = stepDTO.Name,
                TaskId = stepDTO.TaskId,
                IsDone = stepDTO.IsDone
            };
            Context.Steps.Update(step);
            Context.SaveChanges();
        }

        public void DelStep(int? id)
        {
            if (id == null)
                throw new Exception("Step id is null");
            var step = Context.Steps.Find(id);
            Context.Steps.Remove(step);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
