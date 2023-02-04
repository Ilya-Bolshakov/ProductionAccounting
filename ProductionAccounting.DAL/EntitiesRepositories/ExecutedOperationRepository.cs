using Microsoft.EntityFrameworkCore;
using ProductionAccounting.DAL.Context;
using ProductionAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.EntitiesRepositories
{
    public class ExecutedOperationRepository : DbRepository<ExecutedOperation>
    {
        public ExecutedOperationRepository(ProductionAccountingContext productionAccounting) : base(productionAccounting)
        {
        }

        public override IQueryable<ExecutedOperation> Items => base.Items.Include(i => i.Employee)
                                                                         .Include(i => i.Operation);
    }
}
