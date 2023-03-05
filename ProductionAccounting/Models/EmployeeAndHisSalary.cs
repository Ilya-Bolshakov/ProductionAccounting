using ProductionAccounting.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Models
{
    public class EmployeeAndHisSalary : Model
    {
        public EmployeeModel Employee { get; set; }

        public decimal Salary { get; set; }

        public override object Clone()
        {
            EmployeeAndHisSalary employeeAndHisSalary = new()
            {
                Salary = Salary,
                Employee = (EmployeeModel)Employee.Clone()
            };
            return employeeAndHisSalary;
        }
    }
}
