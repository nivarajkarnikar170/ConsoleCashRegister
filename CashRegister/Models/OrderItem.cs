using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashRegister.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }

        public decimal? Weight { get; set; }

        public int? Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderItem(int productId, decimal? weight, int? quantity)
        {
            ProductId = productId;
            Weight = weight;
            Quantity = quantity;
            TotalPrice = weight ?? default(decimal) * quantity ?? default(int);
        }
    }
}