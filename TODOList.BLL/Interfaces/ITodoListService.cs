using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;

namespace TODOList.BLL.Interfaces
{
    public interface ITodoListService
    {
        void MakeTodoList(TodoListDTO listDTO);
        TodoListDTO GetTodoList(int? id);
        IEnumerable<TodoListDTO> GetTodoLists();
        void UpdateTodoList(TodoListDTO listDTO);
        void DelTodoList(int? id);
        void Dispose();
    }
}
