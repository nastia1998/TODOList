using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.DAL.EF;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Services
{
    public class TodoListService : ITodoListService
    {
        IUnitOfWork Database { get; set; }

        public TodoListService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void MakeTodoList(TodoListDTO listDTO)
        {
            throw new NotImplementedException();
        }

        public TodoListDTO GetTodoList(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TodoListDTO> GetTodoLists()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TodoList>, List<TodoListDTO>>(Database.TodoLists.GetAll());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
