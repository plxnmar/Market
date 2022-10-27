using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }

    public class CartItem
    {
        public int Id { get; set; }

        public string CartId { get; set; }
        public Cart Cart { get; set; }

        public int Count { get; set; }

        //public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
