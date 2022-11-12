﻿using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Cart;
using Market.Domain.ViewModels.Order;
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
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<CartItem> cartItemRepository;
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<Product> productRepository;
        private readonly IBaseRepository<Cart> cartRepository;
        private readonly IBaseRepository<Order> orderRepository;
        private readonly IBaseRepository<OrderItem> orderItemRepository;

        public OrderService(IBaseRepository<CartItem> cartItemRepository, IBaseRepository<Product> productRepository,
            IBaseRepository<User> userRepository, IBaseRepository<Cart> cartRepository, IBaseRepository<Order> orderRepository,
            IBaseRepository<OrderItem> orderItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Order>>> GetOrders(string userName)
        {
            var baseResponse = new BaseResponse<IEnumerable<Order>>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<Order>>()
                    {
                        Description = "[GetCartItems] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var order = orderRepository.GetAll().Where(x => x.User.Name == userName);
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = order;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetCartItems] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }


        public async Task<IBaseResponse<OrderViewModel>> CreateOrderViewModel(string userName)
        {
            var baseResponse = new BaseResponse<OrderViewModel>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "[AddOrder] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var order = new Order() { UserId = user.Id };
                var orderItems = CreateOrderItems(user, order);

                var orderViewModel = new OrderViewModel()
                {
                    OrderItems = orderItems,
                    Total = GetTotal(orderItems),
                    ItemsCount = GetCount(orderItems),
                };

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = orderViewModel;

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[AddOrder] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


        public async Task<IBaseResponse<bool>> AddOrder(string userName, OrderViewModel orderViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "[AddOrder] : Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var order = new Order()
                {
                    HasBeenShipped = false,
                    DateCreated = DateTime.Now,
                    Email = orderViewModel.Email,
                    Address = orderViewModel.Address,
                    FirstName = orderViewModel.FirstName,
                    LastName = orderViewModel.LastName,
                    //  OrderItems = orderViewModel.OrderItems,
                    Total = orderViewModel.Total,
                    User = user,
                };

                var orderItems = new List<OrderItem>();

                foreach (var orderItem in orderViewModel.OrderItems)
                {
                    var product = await productRepository.Get(orderItem.ProductId);
                    orderItems.Add(
                        new OrderItem()
                        {
                            Product = product,
                            Price = orderItem.Price,
                            Quantity = orderItem.Quantity,
                        });
                }

                order.OrderItems = orderItems;



                await orderRepository.Create(order);
                baseResponse.StatusCode = StatusCode.OK;

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[AddOrder] : {ex.Message}";
                baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return baseResponse;
        }


        private decimal GetTotal(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Sum(x => x.Price * x.Quantity);
        }

        private int GetCount(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Sum(x => x.Quantity);
        }


        private static List<OrderItem> CreateOrderItems(User? user, Order order)
        {
            var orderItems = new List<OrderItem>();
            var cartItems = user.Cart.CartItems;

            foreach (var cartItem in cartItems)
            {
                orderItems.Add(
                    new OrderItem()
                    {
                        Product = cartItem.Product,
                        ProductId = cartItem.ProductId,
                        Price = cartItem.Product.Price,
                        Quantity = cartItem.Count,
                    });
            }

            order.OrderItems = orderItems;
            return orderItems;
        }
    }
}
