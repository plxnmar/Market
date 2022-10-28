using Azure;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> AddCartItem(int productId)
        {
            var response = await cartService.AddCartItem(User.Identity.Name, productId);

            return RedirectToAction("Index", "Home");
          //  return View();
          
        }
      
    }
}
