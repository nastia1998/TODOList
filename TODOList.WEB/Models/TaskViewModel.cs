using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOList.WEB.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCompletion { get; set; }
        public DateTime DateReminder { get; set; }
        public int TodoListId { get; set; }
    }
}
