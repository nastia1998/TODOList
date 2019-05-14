using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO Authenticate(string username, string password);
        IEnumerable<UserDTO> GetAll();
        Task<UserDTO> GetById(int id);
        Task<UserDTO> Create(UserDTO user, string password);
        void Update(UserDTO user, string password = null);
        Task<UserDTO> Delete(int id);
        string GetToken(string secret, UserDTO user);
    }
}
