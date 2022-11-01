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
        [MinLength(1, ErrorMessage = "Название должно иметь длину более 1 символа")]
        [MaxLength(60, ErrorMessage = "Название должно иметь длину менее 60 символов")]
        public string? Name { get; set; }

        [MaxLength(800, ErrorMessage = "Описание должно иметь длину менее 800 символов")]
        public string? Description { get; set; }

        //[RegularExpression("([0-9]+)")]
        public decimal Price { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public string? ImgPath { get; set; }
        public IFormFile? UploadedImage { get; set; }
    }
}
