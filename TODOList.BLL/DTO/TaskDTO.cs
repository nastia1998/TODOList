using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.DAL.Entities;

namespace TODOList.BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public DateTime DateCompletion { get; set; }
        public DateTime DateReminder { get; set; }

        public int TodoListId { get; set; }

    }
}
