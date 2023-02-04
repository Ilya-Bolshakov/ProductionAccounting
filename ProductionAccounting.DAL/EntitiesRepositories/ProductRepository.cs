using Microsoft.EntityFrameworkCore;
using ProductionAccounting.DAL.Context;
using ProductionAccounting.DAL.Entities;

namespace ProductionAccounting.DAL.EntitiesRepositories
{
    public class ProductRepository : DbRepository<Product>
    {
        public override IQueryable<Product> Items => base.Items.Include(i => i.Operations);
        public ProductRepository(ProductionAccountingContext context) : base(context)
        {
        }
    }
}
