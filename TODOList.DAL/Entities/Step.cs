using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.DAL.Entities
{
    public class Step
    {
        public int Id { get; set; }

        public bool IsDone { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }
    }
}
