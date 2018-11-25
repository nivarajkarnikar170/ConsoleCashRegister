using CashRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashRegister.Service.Implementation
{
    public class OrderService
    {
        public void AddOrderItem(Product product, decimal? weight, int? quantity, Order order) {
            var orderItem = new OrderItem(product.ProductID, weight, quantity);
            order.OrderItems.Add(orderItem);
        }

        public void RemoveLastOrderItem(Order order)
        {
            var orderItemSize = order.OrderItems.Count;
            if (orderItemSize != 0) {
                order.OrderItems.RemoveAt(orderItemSize-1);
            }
            
        }

        public decimal ComputeTotal(List<OrderItem> orderItems) {
            return orderItems != null ? orderItems.Sum(o => o.TotalPrice) : 0;
        }
    }
}