using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOList.WEB.Models
{
    public class StepViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int TaskId { get; set; }
    }
}
