using Azure;
using Market.Domain.Entity;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Market.Controllers
{
    [Authorize]
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
            var response = await cartService.GetCart(User.Identity.Name);
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
                return NoContent();
            }
            return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(int Count, int id)
        {
            var response = await cartService.UpdateCartItem(User.Identity.Name, id, Count);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetCartListPartial");
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCartItem(int cartId, bool isDelete)
        {
            var response = await cartService.DeleteCartItem(User.Identity.Name, cartId, isDelete);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("_CartTotalSum", response.Data);
            }
            return NoContent();
        }

        //[HttpPost]
        //public async Task<IActionResult> DecreaseCartItem(int cartId)
        //{
        //    var response = await cartService.DecreaseCartItem(User.Identity.Name, cartId);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //    }
        //    return NoContent();
        //}




        [HttpGet]
        public async Task<IActionResult> GetCartTotalPartial()
        {
            var response = await cartService.GetCart(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("_CartTotalSum", response.Data);
            }
            return PartialView("_CartTotalSum", response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartListPartial()
        {
            var response = await cartService.GetCart(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("_CartItemList", response.Data.CartItems);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemQuantityPartial(int id)
        {
            // id - productID
            var response = await cartService.GetCartItem(User.Identity.Name, id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
         
                ViewBag.ProductId = id;  
                return PartialView("_CartItemQuantity", response.Data);
            }
            return NoContent();
        }

    }
}
