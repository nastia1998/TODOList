using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<T> Create(T obj);
        Task<T> Get(int id);
        IEnumerable<T> GetAll();
        Task<int> Update(int id, T obj);
        Task<T> Delete(int id);
    }
}
