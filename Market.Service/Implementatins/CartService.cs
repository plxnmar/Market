using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Cart;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly IBaseRepository<CartItem> cartItemRepository;
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Product> productRepository;
        private readonly IBaseRepository<Cart> cartRepository;

        public CartService(IBaseRepository<CartItem> cartItemRepository, IBaseRepository<Product> productRepository,
            IBaseRepository<User> userRepository, IBaseRepository<Cart> cartRepository)
        {
            this.cartItemRepository = cartItemRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
        }

        public async Task<IBaseResponse<IEnumerable<CartItem>>> AddCartItem(string userName, int productId)
        {
            var baseResponse = new BaseResponse<IEnumerable<CartItem>>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<CartItem>>()
                    {
                        Desciption = "[AddCartItem] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var product = await productRepository.Get(productId);

                if (product == null)
                {
                    return new BaseResponse<IEnumerable<CartItem>>()
                    {
                        Desciption = "[AddCartItem] : Продукт не найден",
                        //StatusCode = StatusCode.UserNotFound,
                    };
                }


                var cart = new CartItem()
                {
                    Product = product,
                    Cart = user.Cart,
                    Count = 1,
                };



                var result = await cartItemRepository.Create(cart);

                //await cartRepository.Update(user.Cart);


                //var Carts = cartRepository.GetAll().ToList();

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = user.Cart.CartItems;

            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[AddCartItem] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<CartViewModel>> GetCart(string userName)
        {
            var baseResponse = new BaseResponse<CartViewModel>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<CartViewModel>()
                    {
                        Desciption = "[GetCartItems] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                if (user.Cart == null)
                {
                    var cart = await cartRepository.Create(new Cart() { User = user, CartItems = new List<CartItem>() });
                }

                var cartItems = user.Cart.CartItems.ToList();

                var viewModel = new CartViewModel
                {
                    CartItems = cartItems,
                    CartTotalSum = GetTotal(cartItems)
                };


                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = viewModel;
            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[GetCartItems] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<CartViewModel>> DeleteCartItem(string userName, int cartItemId)
        {
            var baseResponse = new BaseResponse<CartViewModel>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<CartViewModel>()
                    {
                        Desciption = "[DeleteCartItem] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var cartItem = await cartItemRepository.GetAll().FirstOrDefaultAsync(x => x.Id == cartItemId);

                if (cartItem == null)
                {
                    return new BaseResponse<CartViewModel>()
                    {
                        Desciption = "[DeleteCartItem] : Товар в корзине не найден",
                        //  StatusCode = StatusCode.UserNotFound,
                    };
                }

                var result = await cartItemRepository.Remove(cartItem);

                baseResponse.StatusCode = StatusCode.OK;

                var cartItems = user.Cart.CartItems.ToList();

                var viewModel = new CartViewModel
                {
                    CartItems = cartItems,
                    CartTotalSum = GetTotal(cartItems)
                };

                baseResponse.Data = viewModel;

            }
            catch (Exception ex)
            {
                baseResponse.Desciption = $"[DeleteCartItem] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        private decimal GetTotal(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Sum(x => x.Product.Price * x.Count);
        }
    }
}
