using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина 2 символа")]
        [MaxLength(50, ErrorMessage = "максимальная длина 50 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [MinLength(2, ErrorMessage = "Минимальная длина 2 символа")]
        [MaxLength(50, ErrorMessage = "максимальная длина 70 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        [MinLength(3, ErrorMessage="Минимальная длина 3 символа")]
        [MaxLength(80, ErrorMessage = "максимальная длина 80 символов")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите почту")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Неверный формат")]
        public string Email { get; set; }
        public decimal Total { get; set; }
        public int ItemsCount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
