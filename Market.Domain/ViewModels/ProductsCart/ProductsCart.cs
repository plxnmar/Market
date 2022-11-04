using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels.ProductsCart
{
    //public class ProductsCartItems
    //{
    //    //public List<Market.Domain.Entity.Product> Products { get; set; }
    //    //public List<CartItem> CartItems { get; set; }
    //}


    public class ProductCartItem
    {
        public Market.Domain.Entity.Product Product { get; set; }
        public CartItem CartItem { get; set; }

    }

}
