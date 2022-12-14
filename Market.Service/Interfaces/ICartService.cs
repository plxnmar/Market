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

        Task<IBaseResponse<CartViewModel>> GetCart(string userName);
        Task<IBaseResponse<CartItem>> AddCartItem(string userName, int itemId);
        Task<IBaseResponse<CartItem>> GetCartItem(string userName, int productId);
        Task<IBaseResponse<CartViewModel>> DeleteCartItem(string userName, int cartItemId, bool isFullRemove);
        Task<IBaseResponse<IEnumerable<CartItem>>> UpdateCartItem(string userName, int cartItemId, int quantity);

    }
}
