using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Cart;
using Market.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface IOrderService
    {

        Task<IBaseResponse<OrderViewModel>> CreateOrderViewModel(string userName);
        Task<IBaseResponse<bool>> AddOrder(string userName, OrderViewModel orderViewModel);
        //Task<IBaseResponse<OrderViewModel>> GetOrderViewModel(int id, string userName);

        //Task<IBaseResponse<CartViewModel>> GetCart(string userName);
        //Task<IBaseResponse<CartItem>> AddCartItem(string userName, int itemId);
        //Task<IBaseResponse<CartItem>> GetCartItem(string userName, int productId);
        //Task<IBaseResponse<CartViewModel>> DeleteCartItem(string userName, int cartItemId, bool isFullRemove);
        //Task<IBaseResponse<CartItem>> DecreaseCartItem(string userName, int cartItemId);
        //Task<IBaseResponse<IEnumerable<CartItem>>> UpdateCartItem(string userName, int cartItemId, int quantity);

    }
}
