using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.DAL.EF;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Services
{
    public class StepService : IStepService
    {
        IUnitOfWork Database { get; set; }

        public StepService(IUnitOfWork uow)
        {
            Database = uow;
        }

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
            Database.Steps.Create(step);
            Database.Save();
        }

        public StepDTO GetStep(int? id)
        {
            if (id == null)
                throw new Exception("Id of step is null");
            var step = Database.Steps.Get(id.Value);
            if (step == null)
                throw new Exception("Step wasn't found");
            return new StepDTO { Name = step.Name, IsDone = step.IsDone };
        }

        public IEnumerable<StepDTO> GetSteps()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Step, StepDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Step>, List<StepDTO>>(Database.Steps.GetAll());
        }

        public void UpdateStep(StepDTO stepDTO)
        {
            throw new NotImplementedException();
        }

        public void DelStep(int? id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
