using ConsoleCashRegister.Models;
using System.Linq;

namespace ConsoleCashRegister.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private Order order;
        public OrderService(Order order)
        {
            this.order = order;
        }

        public Order GetOrder() {
            return order;
        }
        public void AddOrderItem(Product product, decimal? weight, int? quantity) {
            var orderItem = new OrderItem(product, weight, quantity);
            order.OrderItems.Add(orderItem);
        }

        public void RemoveLastOrderItem()
        {
            var orderItemSize = order.OrderItems.Count;
            if (orderItemSize != 0) {
                order.OrderItems.RemoveAt(orderItemSize-1);
            }            
        }

        public void ComputeTotal() {
            
            order.TotalDiscount = order.OrderItems != null ? order.OrderItems.Sum(o => o.TotalDiscount) : 0;
            order.TotalAmount = (order.OrderItems != null ? order.OrderItems.Sum(o => o.TotalPrice) : 0) - order.TotalDiscount;
        }

        public void ClearOrder() {
            order.OrderItems.Clear();
            order.TotalAmount = 0;
            order.TotalDiscount = 0;
        }
    }
}