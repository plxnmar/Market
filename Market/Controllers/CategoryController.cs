using Azure;
using Market.Domain.Entity;
using Market.Service.Implementatins;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Market.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = await categoryService.GetCategories();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewBag.Categories = response.Data;
                return View(response.Data.ToList());
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            var response = await categoryService.GetCategory(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                //ViewBag.Categories = response.Data;

               // return RedirectToAction("Index");
               // return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryName(int id)
        {
            var response = await categoryService.GetCategoryName(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                //ViewBag.Categories = response.Data;

                return PartialView("_CategoryName", response.Data);
                // return RedirectToAction("Index");
                // return View(response.Data);
            }
            return RedirectToAction("Error");
        }

    }
}
