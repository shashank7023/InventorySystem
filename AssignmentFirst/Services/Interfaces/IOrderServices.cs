using AssignmentFirst.Models;

namespace AssignmentFirst.Services.Interfaces
{
    public interface IOrderService
    {
        void PlaceOrder(int productId, int quantity);
        double GetTotalRevenue();
        IEnumerable<Order> GetOrders();
    }
}
