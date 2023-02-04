using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class ExecutedOperation : Entity
    {
        public Employee Employee { get; set; }
        public Operation Operation { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int CountExecutedOperation { get; set; }
    }
}
