using AssignmentFirst.DbLayer;
using AssignmentFirst.Models;
using AssignmentFirst.Services.Interfaces;

namespace AssignmentFirst.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly InventoryContext _context;

        public ProductService(InventoryContext context)
        {
            _context = context;
        }
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    throw new NotImplementedException();
        //}
        public IEnumerable<Product> GetAllProducts(string sortField = null, string filterField = null, string filterValue = null)
        {
            var products = _context.Products.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "name":
                        products = products.Where(p => p.Name.Contains(filterValue, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "price":
                        if (double.TryParse(filterValue, out var price))
                            products = products.Where(p => p.Price == price);
                        break;
                    case "stock":
                        if (int.TryParse(filterValue, out var stock))
                            products = products.Where(p => p.Stock == stock);
                        break;
                }
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                products = sortField.ToLower() switch
                {
                    "name" => products.OrderBy(p => p.Name),
                    "price" => products.OrderBy(p => p.Price),
                    "stock" => products.OrderBy(p => p.Stock),
                    _ => products
                };
            }

            return products.ToList();
        }

        public Product? GetProductById(int id) => _context.Products.Find(id);
        public void AddProduct(Product product) { _context.Products.Add(product); _context.SaveChanges(); }
        public void UpdateProduct(Product product) { _context.Products.Update(product); _context.SaveChanges(); }
        public void DeleteProduct(int id) { var product = _context.Products.Find(id); if (product != null) { _context.Products.Remove(product); _context.SaveChanges(); } }

     
    }
}
