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
            throw new NotImplementedException();
        }

        public StepDTO GetStep(int? id)
        {
            throw new NotImplementedException();
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
