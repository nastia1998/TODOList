using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.DAL;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Services
{
    public class TodoListService : ITodoListService
    {
        public TodoListService()
        {
            Context = new Context();
        }

        Context Context { get; set; }

        public void MakeTodoList(TodoListDTO listDTO)
        {
            if (listDTO == null)
                throw new Exception("List equals null");
            TodoList list = new TodoList
            {
                UserId = listDTO.UserId,
                Name = listDTO.Name,
                Description = listDTO.Description
            };
            Context.TodoLists.Add(list);
            Context.SaveChanges();
        }

        public TodoListDTO GetTodoList(int? id)
        {
            if (id == null)
                throw new Exception("id of todolist is null");
            var list = Context.TodoLists.Find(id.Value);
            if (list == null)
                throw new Exception("List wasn't found");
            return new TodoListDTO { Name = list.Name, Description = list.Description };
        }

        public IEnumerable<TodoListDTO> GetTodoLists()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TodoList>, List<TodoListDTO>>(Context.TodoLists.ToList());
        }

        public void UpdateTodoList(TodoListDTO listDTO)
        {
            if (listDTO == null)
                throw new Exception("TodoList == null");
            TodoList list = new TodoList
            {
                Id = listDTO.Id,
                UserId = listDTO.UserId,
                Name = listDTO.Name,
                Description = listDTO.Description
            };
            Context.TodoLists.Update(list);
            Context.SaveChanges();
        }

        public void DelTodoList(int? id)
        {
            if (id == null)
                throw new Exception("TodoList id is null");
            var list = Context.TodoLists.Find(id);
            Context.TodoLists.Remove(list);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
