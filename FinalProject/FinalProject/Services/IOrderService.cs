using FinalProject.Models;


namespace FinalProject.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void AddOrder(Order order);
        void EditOrder(Order order);

        void DeleteOrder(int id);
    }
}
