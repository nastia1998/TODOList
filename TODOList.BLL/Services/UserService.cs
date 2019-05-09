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
    public class UserService : IUserService
    {
        public UserService()
        {
            Context = new Context();
        }

        Context Context { get; set; }

        public void MakeUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new Exception("User equals null");
            User user = new User
            {
                Login = userDTO.Login,
                Password = userDTO.Password
            };
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
                throw new Exception("id of User is null");
            var user = Context.Users.Find(id.Value);
            if (user == null)
                throw new Exception("The user wasn't found");
            return new UserDTO { Login = user.Login, Password = user.Password };
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Context.Users.ToList());
        }

        public void UpdateUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new Exception("User equals null");
            User user = new User
            {
                Id = userDTO.Id,
                Login = userDTO.Login,
                Password = userDTO.Password
            };
            Context.Users.Update(user);
            Context.SaveChanges();
        }

        public void DelUser(int? id)
        {
            if (id == null)
                throw new Exception("User id is null");
            var user = Context.Users.Find(id);
            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
