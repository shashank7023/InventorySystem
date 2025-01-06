using AssignmentFirst.DbLayer;
using AssignmentFirst.Services.Implementation;
using AssignmentFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentFirst.Controllers
{
    public class OrderController : Controller
    {
        private readonly InventoryContext _context;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService,IProductService productService, InventoryContext context)
        {
            _orderService = orderService;
            _context = context;
            _productService = productService;
        }

        public IActionResult Index() => View(_orderService.GetOrders());

        public IActionResult PlaceOrder()
        {
            // Assuming you have a Product service or repository to fetch products
            var products = _productService.GetAllProducts(); // Replace with your actual method
            ViewBag.Products = products;

            return View();
        }
        public IActionResult Report()
        {
            var totalRevenue = (from order in _context.Orders
                                join product in _context.Products
                                on order.ProductId equals product.Id
                                select new
                                {
                                    Revenue = product.Price * order.Quantity
                                }).Sum(x => x.Revenue);

            ViewData["TotalRevenue"] = totalRevenue;

            return View();
        }
        public IActionResult DeleteOrder(int id)
        {
            // Find the order with the specified ID
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound(); // Return 404 if the order is not found
            }

            // Remove the order
            _context.Orders.Remove(order);

            // Save changes to the database
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to the list view after deletion
        }

        [HttpPost]
        public IActionResult PlaceOrder(int productId, int quantity)
        {
            try
            {
                _orderService.PlaceOrder(productId, quantity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }

}
