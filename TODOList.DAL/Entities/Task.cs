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

        public DateTime DateCompletion { get; set; }
        public DateTime DateReminder { get; set; }

        public bool IsDone { get; set; }

        public int TodoListId { get; set; }

        [ForeignKey("TodoListId")]
        public TodoList TodoList { get; set; }
    }
}
