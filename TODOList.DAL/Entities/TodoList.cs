using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.DAL.Entities
{
    public class TodoList 
    {
        public int Id { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
