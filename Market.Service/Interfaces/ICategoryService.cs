using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IBaseResponse<IEnumerable<Category>>> GetCategories();
        Task<IBaseResponse<Category>> GetCategory(int id);
    }
}
