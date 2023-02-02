using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class Operation : NamedEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public int OperationDuration { get; set; }
        public OperationСoefficient OperationСoefficient { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
