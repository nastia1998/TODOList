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

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public string Name { get; set; }

        public int TodoListId { get; set; }

    }
}
