using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
        public int ItemsCount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
