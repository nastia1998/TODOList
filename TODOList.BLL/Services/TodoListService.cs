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
    public class TodoListService : IService<TodoListDTO>
    {
        Context Context { get; set; }

        public TodoListService()
        {
            Context = new Context();
        }

        async public Task<TodoListDTO> Create(TodoListDTO listDTO)
        {
            var list = await Context.TodoLists.AddAsync(new TodoList { UserId = listDTO.UserId, Name = listDTO.Name, Description = listDTO.Description });
            var res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<TodoList, TodoListDTO>(list.Entity) : null;
        }

        async public Task<TodoListDTO> Get(int id)
        {
            var list = await Context.TodoLists.FindAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return list != null ? mapper.Map<TodoList, TodoListDTO>(list) : null;
        }

        public IEnumerable<TodoListDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TodoList>, List<TodoListDTO>>(Context.TodoLists.ToList());
        }

        public IEnumerable<TodoListDTO> GetAll(int userId)
        {
            var lists = Context.TodoLists.Where(x => x.UserId == userId).ToList();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TodoList>, List<TodoListDTO>>(lists);
        }

        async public Task<int> Update(int id, TodoListDTO listDTO)
        {
            var list = await Context.TodoLists.FindAsync(id);
            list.UserId = listDTO.UserId;
            list.Name = listDTO.Name;
            list.Description = listDTO.Description;
            return await Context.SaveChangesAsync();
        }

        async public Task<TodoListDTO> Delete(int id)
        {
            var list = await Context.TodoLists.FindAsync(id);
            if (list == null)
                return null;
            Context.TodoLists.Remove(list);
            int res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoList, TodoListDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<TodoList, TodoListDTO>(list) : null;
        }

    }
}
