using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels.Cart
{
	public class CartViewModel
	{
        public List<CartItem> CartItems { get; set; }
        public decimal CartTotalSum { get; set; }
    }
}
