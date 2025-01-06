using AssignmentFirst.DbLayer;
using AssignmentFirst.Models;
using AssignmentFirst.Services.Interfaces;

namespace AssignmentFirst.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly InventoryContext _context;

        public OrderService(InventoryContext context)
        {
            _context = context;
        }

        public void PlaceOrder(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null || product.Stock < quantity)
                throw new InvalidOperationException("Insufficient stock!");


            product.Stock -= quantity;
            _context.Orders.Add(new Order { ProductId = productId, Quantity = quantity, OrderDate = DateTime.Now });
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrders() => _context.Orders.ToList();
        public double GetTotalRevenue()
        {
            return _context.Orders
                .Join(
                    _context.Products,
                    order => order.ProductId,
                    product => product.Id,
                    (order, product) => new { product.Price, order.Quantity }
                )
                .Sum(o => o.Price * o.Quantity);
        }

    }
}
