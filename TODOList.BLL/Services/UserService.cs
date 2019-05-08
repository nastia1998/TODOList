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
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void MakeUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new Exception("User equals null");
            User user = new User
            {
                Login = userDTO.Login,
                Password = userDTO.Password
            };
            Database.Users.Create(user);
            Database.Save();
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
                throw new Exception("id of User is null");
            var user = Database.Users.Get(id.Value);
            if (user == null)
                throw new Exception("The user wasn't found");
            return new UserDTO { Login = user.Login, Password = user.Password };
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
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
            Database.Users.Update(user);
        }

        public void DelUser(int? id)
        {
            if (id == null)
                throw new Exception("User id is null");
            Database.Users.Delete(id.Value);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
