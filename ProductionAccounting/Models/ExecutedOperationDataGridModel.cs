using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Models
{
    public class ExecutedOperationDataGridModel : Model
    {
        public EmployeeModel Employee { get; set; }
        public OperationModel Operation { get; set; }
        public int CountExecutedOperation { get; set; }
        public decimal Total { get; private set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public ExecutedOperationDataGridModel(ExecutedOperation orm)
        {
            Id = orm.Id;
            Employee = new EmployeeModel(orm.Employee);
            Operation = new OperationModel(orm.Operation);
            CountExecutedOperation = orm.CountExecutedOperation;
            Total = CountExecutedOperation * Operation.Price;
            Year = orm.Year;
            Month = orm.Month;
        }

        public ExecutedOperationDataGridModel()
        { }

        public override object Clone()
        {
            var model = new ExecutedOperationDataGridModel()
            {
                Employee = (EmployeeModel)Employee.Clone(),
                Operation = (OperationModel)Operation.Clone(),
                CountExecutedOperation = CountExecutedOperation
            };
            return model;
        }
    }
}
