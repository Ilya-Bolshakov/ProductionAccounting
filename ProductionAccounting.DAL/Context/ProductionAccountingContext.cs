using Microsoft.EntityFrameworkCore;
using ProductionAccounting.DAL.Entities;

namespace ProductionAccounting.DAL.Context
{
    public class ProductionAccountingContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationСoefficient> OperationСoefficients { get; set; }
        public DbSet<Product> Products { get; set; }
        public ProductionAccountingContext(DbContextOptions<ProductionAccountingContext> options) : base(options)
        {

        }
    }
}
