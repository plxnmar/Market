using Market.DAL.Interfaces;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await productService.GetProducts();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await productService.GetProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.DeleteProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("Error");
        }

        //  [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> SaveProduct(int id)
        {
            //новый объект
            if (id == 0)
                return View();

            var response = await productService.GetProduct(id);

            var categories = (await categoryService.GetCategories()).Data.ToList();
            ViewBag.Categories = categories;

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductViewModel model)
        {
            //if (ModelState.IsValid)
            //{

            model.Category = (await categoryService.GetCategory(model.CategoryId)).Data;

            if (model.Id == 0)
            {
               
                await productService.AddProduct(model);
            }
            else
            {
                await productService.EditProduct(model.Id, model);
            }
            //}

            return RedirectToAction("GetProducts");
        }
    }
}
