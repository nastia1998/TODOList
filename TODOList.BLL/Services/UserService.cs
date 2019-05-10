using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class UserService : IService<UserDTO>
    {
        Context Context { get; set; }

        public UserService()
        {
            Context = new Context();
        }

        async public Task<UserDTO> Create(UserDTO userDTO)
        {
            var user = await Context.Users.AddAsync(new User {Login = userDTO.Login, Password = userDTO.Password });
            var res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<User, UserDTO>(user.Entity) : null;
        }

        async public Task<UserDTO> Get(int id)
        {
            var user = await Context.Users.FindAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return user != null ? mapper.Map<User, UserDTO>(user) : null;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var mapper =  new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Context.Users.ToList());
        }

        async public Task<int> Update(int id, UserDTO userDTO)
        {
            var user = await Context.Users.FindAsync(id);
            user.Login = userDTO.Login;
            user.Password = userDTO.Password;
            return await Context.SaveChangesAsync();
        }

        async public Task<UserDTO> Delete(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null)
                return null;
            Context.Users.Remove(user);
            int res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<User, UserDTO>(user) : null;

        }

    }
}
