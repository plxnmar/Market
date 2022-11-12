using Azure;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Order;
using Market.Domain.ViewModels.Product;
using Market.Service.Implementatins;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.IO;

namespace Market.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await orderService.GetOrders(User.Identity.Name);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> SaveOrder(int id)
        {
            var response = await orderService.CreateOrderViewModel(User.Identity.Name);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [HttpPost]
        public async Task<IActionResult> SaveOrder(OrderViewModel orderViewModel)
        {
            //для отмены валидации списка
            for (int i = 0; i < orderViewModel.OrderItems.Count; i++)
            {
                ModelState.Remove("OrderItems[" + i + "].Order");
            }

            if (ModelState.IsValid)
            {
                var response = await orderService.AddOrder(User.Identity.Name, orderViewModel);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(orderViewModel);
                }
            }
            return View(orderViewModel);
        }
    }
}
