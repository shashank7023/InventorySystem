using AssignmentFirst.DbLayer;
using AssignmentFirst.Models;
using AssignmentFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentFirst.Controllers
{
    public class ProductController : Controller
    {
        private readonly InventoryContext _context;

        public ProductController(InventoryContext context)
        {
            _context = context;
        }

        // Display the list of products
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        

        private IQueryable<Product> ApplyFilters(IQueryable<Product> products, string price, string stock)
        {
            // Filter by price if specified
            if (!string.IsNullOrEmpty(price) && double.TryParse(price, out var priceValue))
            {
                products = products.Where(p => p.Price <= priceValue);
            }

            // Filter by stock if specified
            if (!string.IsNullOrEmpty(stock) && int.TryParse(stock, out var stockValue))
            {
                products = products.Where(p => p.Stock >= stockValue);
            }

            return products;
        }
        //public IActionResult Index(string price, string stock, string sortBy)
        //{
        //    var products = _context.Products.AsQueryable();

        //    // Filtering
        //    if (!string.IsNullOrEmpty(price) && decimal.TryParse(price, out var priceValue))
        //    {
        //        products = products.Where(p => p.Price <= priceValue);
        //    }

        //    if (!string.IsNullOrEmpty(stock) && int.TryParse(stock, out var stockValue))
        //    {
        //        products = products.Where(p => p.Stock >= stockValue);
        //    }

        //    // Sorting
        //    if (sortBy == "price")
        //    {
        //        products = products.OrderBy(p => p.Price);
        //    }
        //    else if (sortBy == "stock")
        //    {
        //        products = products.OrderBy(p => p.Stock);
        //    }

        //    return View(products.ToList());
        //}


        // Display the create product form
        public IActionResult Create()
        {
            return View();
        }

        // Handle create product post
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Display the edit product form
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Handle edit product post
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Display the delete product confirmation form
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Handle delete product post
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

    //public class ProductController : Controller
    //{
    //    private readonly IProductService _productService;

    //    public ProductController(IProductService productService)
    //    {
    //        _productService = productService;
    //    }

    //    public IActionResult Index() => View(_productService.GetAllProducts());

    //    public IActionResult Create() => View();

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult Create(Product product)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _productService.AddProduct(product); // Add the product to the database
    //            return RedirectToAction("Index"); // Redirect to the product list
    //        }

    //        return View(product); // Return the form with validation errors
    //    }

    //    public IActionResult Edit(int id)
    //    {
    //        var product = _productService.GetProductById(id);
    //        if (product == null) return NotFound();
    //        return View(product);
    //    }

    //    [HttpPost]
    //    public IActionResult Edit(Product product)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _productService.UpdateProduct(product);
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(product);
    //    }

    //    public IActionResult Delete(int id)
    //    {
    //        _productService.DeleteProduct(id);
    //        return RedirectToAction(nameof(Index));
    //    }
    //}

}
