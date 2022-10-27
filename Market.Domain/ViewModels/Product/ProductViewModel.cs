using Market.Domain.Entity;
using Market.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Введите стоимость")]
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string? ImgPath { get; set; }
        public IFormFile? UploadedImage { get; set; }

        //    public TypeProduct TypeProduct { get; set; }
    }
}
