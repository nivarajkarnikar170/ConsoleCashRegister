using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashRegister.Models
{
    public class Order
    {
        public List<OrderItem> OrderItems;

        private decimal TotalDiscount;
       
        public decimal TotalAmount;

    }
}