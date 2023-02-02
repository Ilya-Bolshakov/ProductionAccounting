using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class Product : NamedEntity
    {
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
