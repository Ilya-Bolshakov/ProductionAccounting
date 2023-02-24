using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IAddingJobDataService
    {
        public int? GetNumberExecutedOperations(int employeeId, int operationId, int month, int year);
        public Task InsertData(int employeeId, int operationId, int month, int year, int numberOfOperations);
    }
}
