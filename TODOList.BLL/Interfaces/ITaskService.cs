using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;

namespace TODOList.BLL.Interfaces
{
    public interface ITaskService
    {
        void MakeTask(TaskDTO taskDTO);
        TaskDTO GetTask(int? id);
        IEnumerable<TaskDTO> GetTasks();
        void UpdateTask(TaskDTO taskDTO);
        void DelTask(int? id);
        void Dispose();
    }
}
