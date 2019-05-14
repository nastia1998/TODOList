using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.DAL.Entities
{
    public class TodoList 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
