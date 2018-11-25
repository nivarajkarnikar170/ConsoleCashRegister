using System.Collections.Generic;

namespace ConsoleCashRegister.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public List<OrderItem> OrderItems;

        public decimal TotalDiscount;

        public decimal TotalAmount;       

    }
}