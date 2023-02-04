using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class Employee : NamedEntity
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
