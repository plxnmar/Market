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
        Task<IBaseResponse<IEnumerable<Order>>> GetOrders(string userName);
    }
}
