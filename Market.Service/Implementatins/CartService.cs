using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Implementatins
{
    public class CartService : ICartService
    {
        private readonly IBaseRepository<Cart> cartItemRepository;
        private readonly IBaseRepository<User> userRepository;

        public CartService(IBaseRepository<Cart> cartItemRepository, IBaseRepository<User> userRepository)
        {
            this.cartItemRepository = cartItemRepository;
            this.userRepository = userRepository;
        }

        public async Task<IBaseResponse<IEnumerable<CartItem>>> GetCartItems(string userName)
        {
            var baseResponse = new BaseResponse<IEnumerable<CartItem>>();
            try
            {
                var user = userRepository.GetAll().FirstOrDefault(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<CartItem>>()
                    {
                        Desciption = "[GetCartItems] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var cartItems = user.Cart.CartItems;
                //if (cart == null)
                //{
                //    baseResponse.Desciption = "[GetCartItems] : Корзины не найдено";

                //}

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                baseResponse.Data = cartItems;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetCartItems] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }

            return baseResponse;
        }
    }
}
