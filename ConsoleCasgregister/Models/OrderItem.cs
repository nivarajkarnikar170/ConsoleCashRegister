namespace ConsoleCashRegister.Models
{
    public class OrderItem
    {
        public OrderItem(Product product, decimal? weight, int? quantity)
        {
            Product = product;

            Weight = weight;
            Quantity = quantity;
            TotalPrice = Weight.HasValue ? Weight.Value * product.UnitPrice : Quantity.Value * product.UnitPrice;
        }
        public Product Product {get; set;}

        public decimal? Weight { get; set; }

        public int? Quantity { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPrice { get; set; }
        
    }
}