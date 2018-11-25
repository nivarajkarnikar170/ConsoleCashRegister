namespace ConsoleCashRegister.Models
{
    public class Product
    {
        public Product(int productID, string productName, decimal unitPrice)
        {
            ProductID = productID;
            ProductName = productName;
            UnitPrice = unitPrice;
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }       
    }
}