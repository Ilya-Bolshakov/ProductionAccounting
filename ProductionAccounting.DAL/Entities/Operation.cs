using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class Operation : NamedEntity
    {
        public decimal Cost { get; set; }
        public int OperationDuration { get; set; }
        public OperationСoefficient OperationСoefficient { get; set; }
        public List<Product> Products { get; set; }
    }
}
