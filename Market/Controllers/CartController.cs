using Azure;
using Market.Domain.Entity;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Market.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var response = await cartService.GetCartItems(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(int productId)
        {
            var response = await cartService.AddCartItem(User.Identity.Name, productId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetProducts", "Product");
            }

            return RedirectToAction("Index", "Home");
            //  return View();

        }



        [HttpPost]
        public async Task<IActionResult> DeleteCartItem(int cartId)
        {
            var response = await cartService.DeleteCartItem(User.Identity.Name, cartId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                //return View("/");
                return RedirectToAction("GetCart");
            }
             return RedirectToAction("Index", "Home");
        }

    }
}
