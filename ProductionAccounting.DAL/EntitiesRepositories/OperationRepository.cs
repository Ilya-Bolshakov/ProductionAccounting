using Microsoft.EntityFrameworkCore;
using ProductionAccounting.DAL.Context;
using ProductionAccounting.DAL.Entities;

namespace ProductionAccounting.DAL.EntitiesRepositories
{
    public class OperationRepository : DbRepository<Operation>
    {
        public override IQueryable<Operation> Items => base.Items.Include(i => i.OperationСoefficient);
        public OperationRepository(ProductionAccountingContext context) : base(context)
        {
        }
    }
}
