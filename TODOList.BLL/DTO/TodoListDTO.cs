using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.BLL.DTO
{
    public class TodoListDTO
    {
        public int Id { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
