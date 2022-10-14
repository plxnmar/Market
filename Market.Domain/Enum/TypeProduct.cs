using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Enum
{
    public enum TypeProduct
    {
        [Display(Name = "Овощи")]
        Vegetables = 0,
        [Display(Name = "Фрукты")]
        Fruits = 1,
        [Display(Name = "Мясо")]
        Meat = 2,
        [Display(Name = "Рыба")]
        Fish = 3,
        [Display(Name = "Выпечка")]
        Bakery = 4,
    }
}
