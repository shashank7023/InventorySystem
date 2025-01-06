using AssignmentFirst.Models;

namespace AssignmentFirst.Services.Interfaces
{
    public interface IProductService
    {
        //IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        public IEnumerable<Product> GetAllProducts(string sortField = null, string filterField = null, string filterValue = null);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
