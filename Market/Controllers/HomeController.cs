using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        private readonly IProductRepository productRepository;
        public async Task<IActionResult> Index()
        {
            var response = await productRepository.GetAll();
            return View();
            //Product product = new Product()
            //{
            //    Name = "Яблоко",
            //    Price = 100
            //};
            //  return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}