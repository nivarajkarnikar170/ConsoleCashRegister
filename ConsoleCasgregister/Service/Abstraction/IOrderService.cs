using ConsoleCashRegister.Models;

namespace ConsoleCashRegister.Service.Implementation
{
    public interface IOrderService
    {
        Order GetOrder();
        void AddOrderItem(Product product, decimal? weight, int? quantity);

        void RemoveLastOrderItem();

        void ComputeTotal();

        void ClearOrder();
    }
}