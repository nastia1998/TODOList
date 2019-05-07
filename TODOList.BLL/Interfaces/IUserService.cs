using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;

namespace TODOList.BLL.Interfaces
{
    public interface IUserService
    {
        void MakeUser(UserDTO stepDTO);
        UserDTO GetUser(int? id);
        IEnumerable<UserDTO> GetUsers();
        void UpdateUser(UserDTO userDTO);
        void DelUser(int? id);
        void Dispose();
    }
}
