using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Cart;
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
        Task<IBaseResponse<CartViewModel>> GetCart(string userName);
        Task<IBaseResponse<IEnumerable<CartItem>>> AddCartItem(string userName, int itemId);
        Task<IBaseResponse<CartViewModel>> DeleteCartItem(string userName, int cartItemId);
        Task<IBaseResponse<IEnumerable<CartItem>>> UpdateCartItem(string userName, int cartItemId, int quantity);
        //Task<IBaseResponse<Product>> DeleteProduct(int id);
        //Task<IBaseResponse<bool>> AddProduct(ProductViewModel productViewModel);
        //Task<IBaseResponse<Product>> EditProduct(int id, ProductViewModel model);
    }
}
