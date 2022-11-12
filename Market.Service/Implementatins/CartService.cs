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
using System.ComponentModel.DataAnnotations;
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

        public async Task<IBaseResponse<CartItem>> AddCartItem(string userName, int productId)
        {
            //var baseResponse = new BaseResponse<CartItem>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<CartItem>()
                    {
                        Description = "[AddCartItem] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }


                var product = await productRepository.Get(productId);

                if (product == null)
                {
                    return new BaseResponse<CartItem>()
                    {
                        Description = "[AddCartItem] : Продукт не найден",
                        //StatusCode = StatusCode.UserNotFound,
                    };
                }

                //    var cartItem = await user.Cart.CartItems..FirstOrDefaultAsync(x => x.ProductId == productId);
                var all = cartItemRepository.GetAll();
                var cartItem = await all.FirstOrDefaultAsync(x => x.ProductId == productId && x.Cart.UserId == user.Id);

                if (cartItem == null)
                {
                    cartItem = new CartItem()
                    {
                        Product = product,
                        Cart = user.Cart,
                        Count = 1,
                    };

                    var result = await cartItemRepository.Create(cartItem);
                }
                else
                {
                    cartItem.Count++;
                    var result = await cartItemRepository.Update(cartItem);
                }

                return new BaseResponse<CartItem>()
                {
                    StatusCode = StatusCode.OK,
                    //  baseResponse.Data = user.Cart.CartItems;
                    Data = cartItem,
                };


            }
            catch (Exception ex)
            {
                return new BaseResponse<CartItem>()
                {
                    Description = $"[AddCartItem] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                };

            }
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
                        Description = "[GetCartItems] : Пользователь не найден",
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
                    CartTotalSum = GetTotal(cartItems),
                    CartItemsCount = GetCount(cartItems)
                };


                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = viewModel;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetCartItems] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<CartItem>> GetCartItem(string userName, int productId)
        {
            var baseResponse = new BaseResponse<CartItem>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<CartItem>()
                    {
                        Description = "[GetCartItems] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                if (user.Cart == null)
                {
                    var cart = await cartRepository.Create(new Cart() { User = user, CartItems = new List<CartItem>() });
                }

                //var cartItem = user.Cart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
                var cartItem = await cartItemRepository.GetAll().FirstOrDefaultAsync(x => x.ProductId == productId && x.Cart.UserId == user.Id);


                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = cartItem;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetCartItems] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<CartViewModel>> DeleteCartItem(string userName, int cartItemId, bool isFullDelete)
        {
            var baseResponse = new BaseResponse<CartViewModel>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<CartViewModel>()
                    {
                        Description = "[DeleteCartItem] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var cartItem = await cartItemRepository.GetAll().FirstOrDefaultAsync(x => x.Id == cartItemId);

                if (cartItem == null)
                {
                    return new BaseResponse<CartViewModel>()
                    {
                        Description = "[DeleteCartItem] : Товар в корзине не найден",
                        //  StatusCode = StatusCode.UserNotFound,
                    };
                }

                if (isFullDelete || cartItem.Count == 1)
                {
                    var result = await cartItemRepository.Remove(cartItem);
                }
                else
                {
                    cartItem.Count--;
                    var result = await cartItemRepository.Update(cartItem);
                }


                baseResponse.StatusCode = StatusCode.OK;

                var cartItems = user.Cart.CartItems.ToList();

                var viewModel = new CartViewModel
                {
                    CartItems = cartItems,
                    CartTotalSum = GetTotal(cartItems),
                    CartItemsCount = GetCount(cartItems)
                };

                baseResponse.Data = viewModel;

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[DeleteCartItem] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        private decimal GetTotal(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Sum(x => x.Product.Price * x.Count);
        }

        private int GetCount(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Sum(x => x.Count);
        }

        public async Task<IBaseResponse<IEnumerable<CartItem>>> UpdateCartItem(string userName, int cartItemId, int quantity)
        {
            var baseResponse = new BaseResponse<IEnumerable<CartItem>>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<CartItem>>()
                    {
                        Description = "[UpdateCartItem] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }


                var cartItem = user.Cart.CartItems.FirstOrDefault(x => x.Id == cartItemId);

                if (cartItem != null)
                {
                    cartItem.Count = quantity;
                    var result = await cartItemRepository.Update(cartItem);

                }

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = user.Cart.CartItems;

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[UpdateCartItem] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


    }
}
