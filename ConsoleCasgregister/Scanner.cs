using ConsoleCashRegister.Models;
using ConsoleCashRegister.Service.Implementation;
using System;
using System.Collections.Generic;

namespace ConsoleCashRegister
{
    public class Scanner
    {
        private IOrderService orderService;
        public Scanner(IOrderService orderService)
        {
            this.orderService = orderService;

        }        
        public void StartScan() {
            Console.WriteLine("Press any Key to begin scanning items...");
            Console.ReadKey();
            InsertProduct();            
        }
        private void InsertProduct()
        {
            decimal? weight = null;
            int? quantity = null;
            int productId = InputInt("Enter Product Id (Format : Number greater than 0)");
            string productName = InputString("Enter the product Name (Format : alphanumeric)", null);
            decimal unitPrice = InputDecimal("Enter unit price (Format : decimal)");
            string productType = InputString("Enter w for Weight or q for Quantity", new string[] { "w", "q" });
            
            if (productType == "w")
                weight = InputDecimal("Enter Weight (Format : decimal)");
            else
                quantity = InputInt("Enter Quantity (Format : numeric)");

            //add product to order

            var product = new Product(productId, productName, unitPrice);            
            orderService.AddOrderItem(product, weight, quantity);            

            //input more items or end

            Console.WriteLine("\n");
            var choice = InputString("Enter m for adding more product | c to clear entered products | x to exit or add coupons or checkout.", new string[] { "m", "c", "x" });
            if (choice == "m")
                InsertProduct();
            if (choice== "c")
            {
                orderService.ClearOrder();
                StartScan();
            }
            if (choice == "x") {
                var actionOption = InputString("Enter a to add dicount coupon | c to checkout.", new string[] { "a", "d" });
                if (actionOption == "c")
                    DisplayOrder(orderService.GetOrder());
                else {
                    var couponType = InputString("Enter p for percent off type coupon | b for buy 2 get 1 free type coupon.", new string[] { "p", "b" });
                    if (couponType == "p") {
                        var percentOff = InputDecimal("Enter off percent");
                        var discountProductIds = InputIntArray("Enter product Id (Format : Number greater than 0) and enter 0 when you have finished.");
                        ApplyPercentDiscount(percentOff, orderService.GetOrder(), discountProductIds);
                    }
                    else{
                        var discountProductIds = InputIntArray("Enter product Id (Format : Number greater than 0) and enter 0 when you have finished.");
                        ApplyBuyTwoGetOneCoupon(orderService.GetOrder(), discountProductIds);
                    }
                    DisplayOrder(orderService.GetOrder());
                }
            }
        }

        private void ApplyPercentDiscount(decimal percent, Order order, List<int> productIds) {
            
            foreach (var item in order.OrderItems) {
                if (productIds.Contains(item.Product.ProductID))
                {                    
                    item.TotalDiscount = (percent / 100) * item.TotalPrice;
                    Console.WriteLine("\n");                    
                    Console.WriteLine("..................................................Percent Off Discount Detail.........................................................");
                    Console.WriteLine("\n");
                    Console.WriteLine("%" + percent + " Discount applied to Product with id : " + item.Product.ProductID);
                    Console.WriteLine("Final Price : " + (item.TotalPrice - item.TotalDiscount));
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                }
            }
        }

        private void ApplyBuyTwoGetOneCoupon(Order order, List<int> productIds)
        {
            foreach (var item in order.OrderItems)
            {
                if (item.Quantity.HasValue &&  item.Quantity.Value > 2 && productIds.Contains(item.Product.ProductID))
                {
                    int itemsWaived = item.Quantity.Value / 3;
                    item.TotalDiscount = itemsWaived * item.Product.UnitPrice;
                    Console.WriteLine("\n");
                    Console.WriteLine(".........................Buy Two Get one Free Discount Detail......................................");
                    Console.WriteLine("\n");
                    Console.WriteLine("Buy two get one free applied to product with id : " + item.Product.ProductID);
                    Console.WriteLine("Final Price : " + (item.TotalPrice - item.TotalDiscount));
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                }
            }
        }

        private void DisplayOrder(Order order) {

            orderService.ComputeTotal();
            Console.WriteLine("..................................................Order Detail.........................................................");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            foreach (var item in order.OrderItems)
            {
                Console.WriteLine("Product Id: " + item.Product.ProductID);
                Console.WriteLine("Product Name: " + item.Product.ProductName);
                Console.WriteLine("Unit Price: " + item.Product.UnitPrice);
                if (item.Weight.HasValue)
                    Console.WriteLine("Product Weight: " + item.Weight.Value);
                if (item.Quantity.HasValue)
                    Console.WriteLine("Product Quantity: " + item.Quantity.Value);
                Console.WriteLine("Total Item Price: " + item.TotalPrice);
                Console.WriteLine("\n");
            }
            Console.WriteLine("Total Discout " + order.TotalDiscount);
            Console.WriteLine("Total Amount " + order.TotalAmount);

            Console.WriteLine("PLease enter or swipe your card to pay.");
            Console.ReadKey();
        }

        private decimal InputDecimal(string inputMsg)
        {
            int counter = 0;
            do
            {
                if (counter > 0)
                {
                    Console.WriteLine("INVALID FORMAT !!!");
                }
                Console.WriteLine(inputMsg);
                try
                {
                    var value = decimal.Parse(Console.ReadLine());
                    return value;
                }
                catch
                {
                    counter++;
                }
            } while (true);
        }

        private int InputInt(string inputMsg)
        {
            int counter = 0;
            do
            {
                if (counter > 0)
                {
                    Console.WriteLine("INVALID FORMAT !!!");
                }
                Console.WriteLine(inputMsg);
                try
                {
                    var value = int.Parse(Console.ReadLine());
                    return value;
                }
                catch
                {
                    counter++;
                }
            } while (true);
        }

        private string InputString(string inputMsg, string[] match)
        {
            Console.WriteLine(inputMsg);
            var input = Console.ReadLine();

            if (match != null && match.Length > 0)
            {
                foreach (var item in match)
                {
                    if (item == input)
                        return input;
                }
                Console.WriteLine("INVALID FORMAT !!");
                InputString(inputMsg, match);
            }

            return input;
        }

        private List<int> InputIntArray(string inputMsg) {
            int counter = 0;

            List<int> ids = new List<int>();
            do
            {
                if (counter > 0)
                {
                    Console.WriteLine("INVALID FORMAT !!!");
                }
                Console.WriteLine(inputMsg);
                try
                {                    
                    var value = int.Parse(Console.ReadLine());
                    if (value == 0) {
                        return ids;
                    }
                    ids.Add(value);
                }
                catch
                {
                    counter++;
                }
            } while (true);
        }

    }
}
