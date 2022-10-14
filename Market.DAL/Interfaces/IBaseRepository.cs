using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        bool Add(T entity);
        bool Remove(T entity);
        Task<List<T>> GetAll();
    }
}
