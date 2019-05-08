using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.BLL.DTO
{
    public class StepDTO
    {

        public int Id { get; set; }
        public bool IsDone { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
    }
}
