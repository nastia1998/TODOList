using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.DAL.Entities
{
    public class Task 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public int TodoListId { get; set; }

        [ForeignKey("TodoListId")]
        public TodoList TodoList { get; set; }
    }
}
