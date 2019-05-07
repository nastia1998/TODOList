using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;

namespace TODOList.BLL.Interfaces
{
    public interface IStepService
    {
        void MakeStep(StepDTO stepDTO);
        StepDTO GetStep(int? id);
        IEnumerable<StepDTO> GetSteps();
        void UpdateStep(StepDTO stepDTO);
        void DelStep(int? id);
        void Dispose();
    }
}
