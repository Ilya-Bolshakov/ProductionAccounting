using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class OperationСoefficient : NamedEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal CoefficientValue { get; set; }
    }
}
