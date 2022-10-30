using Azure;
using Market.Domain.Entity;
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





    }
}
