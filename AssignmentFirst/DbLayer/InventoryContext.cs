using Microsoft.EntityFrameworkCore;
using AssignmentFirst.Models;

namespace AssignmentFirst.DbLayer
{
    public class InventoryContext : DbContext
    {
        // Constructor required for DbContext configuration
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

}
