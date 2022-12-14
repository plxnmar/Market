using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Get(int id);
        Task<bool> Create(T entity);
        Task<bool> Remove(T entity);
        IQueryable<T> GetAll();
        Task<T> Update(T entity);
    }
}
