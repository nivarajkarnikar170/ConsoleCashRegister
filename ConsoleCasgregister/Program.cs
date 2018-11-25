using ConsoleCashRegister;
using ConsoleCashRegister.Models;
using ConsoleCashRegister.Service.Implementation;

namespace ConsoleCasgregister
{
    class Program
    {
        static void Main(string[] args)
        {
            var scanner = new Scanner(new OrderService(new Order())); //use factory class or dependency injection framework in real world
            scanner.StartScan();           
        }  
    }
}
