using Azure;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Implementatins;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ICartService cartService;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductController(IProductService productService, ICategoryService categoryService,
            IWebHostEnvironment appEnvironment, ICartService cartService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.cartService = cartService;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await productService.GetProducts();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Data.ToList());
                }
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryProducts(int id)
        {
            var response = await productService.GetCategoryProducts(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewBag.CategoryId = id;
                return View("GetProducts", response.Data.ToList());
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await productService.GetProductViewModel(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.DeleteProduct(id, _appEnvironment.WebRootPath);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("Error");
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SaveProduct(int id)
        {
            //получение всех категорий
            var categories = (await categoryService.GetCategories()).Data.ToList();
            ViewBag.Categories = categories;

            //новый объект
            if (id == 0)
            {
                var model = new ProductViewModel();
                return View(model);
            }
            var response = await productService.GetProductViewModel(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Category = (await categoryService.GetCategory(model.CategoryId)).Data;

                if (model.Id == 0)
                {
                    await productService.AddProduct(model, _appEnvironment.WebRootPath);
                }
                else
                {
                    await productService.EditProduct(model.Id, model, _appEnvironment.WebRootPath);
                }

                return RedirectToAction("GetProducts");
            }

            //получение всех категорий
            var categories = (await categoryService.GetCategories()).Data.ToList();
            ViewBag.Categories = categories;
            return View(model);
        }

        //private async Task SaveImage(ProductViewModel model)
        //{
        //    //удаление старого изображения 
        //    DeleteImage(model.ImgPath);

        //    // путь к папке img в root
        //    string path = "/img/" + model.UploadedImage.FileName;

        //    // сохраняем файл в папку img в каталоге wwwroot
        //    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //    {
        //        await model.UploadedImage.CopyToAsync(fileStream);
        //    }

        //    model.ImgPath = path;
        //}


        //private async Task DeleteImage(string path)
        //{
        //    var fullPath = _appEnvironment.WebRootPath + path;

        //    try
        //    {
        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            System.IO.File.Delete(fullPath);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}
    }
}
