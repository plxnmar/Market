using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface ICartService
    {
        //Task<IBaseResponse<IEnumerable<Product>>> GetProducts();
        //Task<IBaseResponse<ProductViewModel>> GetProductViewModel(int id);
        Task<IBaseResponse<IEnumerable<CartItem>>> GetCartItems(string name);
        //Task<IBaseResponse<Product>> DeleteProduct(int id);
        //Task<IBaseResponse<bool>> AddProduct(ProductViewModel productViewModel);
        //Task<IBaseResponse<Product>> EditProduct(int id, ProductViewModel model);
    }
}
