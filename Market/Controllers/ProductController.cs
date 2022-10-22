using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
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
        IWebHostEnvironment _appEnvironment;

        public ProductController(IProductService productService,
            ICategoryService categoryService, IWebHostEnvironment appEnvironment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            _appEnvironment = appEnvironment;
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

        [HttpGet]
        public async Task<IActionResult> GetProductViewModel(int id)
        {
            var response = await productService.GetProductViewModel(id);

          
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.DeleteProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                DelteImage(response);

                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("Error");
        }


        //  [Authorize(Roles = "Admin")]
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

            //var file = "./wwwroot" + response.Data.ImgPath;
            //using var stream = System.IO.File.OpenRead(file);
            ////  var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
            //var File = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            //response.Data.UploadedImage = File;


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

            if (model.UploadedImage != null)
                await SaveImage(model);

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

        private async Task SaveImage(ProductViewModel model)
        {
            // путь к папке img в root
            string path = "/img/" + model.UploadedImage.FileName;

            // сохраняем файл в папку img в каталоге wwwroot
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await model.UploadedImage.CopyToAsync(fileStream);
            }

            model.ImgPath = path;
        }

        private void DelteImage(IBaseResponse<Product> response)
        {
            var fullPath = _appEnvironment.WebRootPath + response.Data.ImgPath;

            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
