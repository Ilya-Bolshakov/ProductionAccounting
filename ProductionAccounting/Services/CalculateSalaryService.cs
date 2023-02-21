using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System.Linq;

namespace ProductionAccounting.Services
{
    public class CalculateSalaryService : ICalculateSalaryService
    {
        IRepository<ExecutedOperation> _repository;
        IRepository<Operation> _operationRepository;
        public CalculateSalaryService(IRepository<ExecutedOperation> repository, IRepository<Operation> operationRepository)
        {
            _repository = repository;
            _operationRepository = operationRepository;
        }

        public decimal CalculateEmployeeSalary(int employeeId, int year, int month)
        {
            var dataEmployee = _repository.Items.Where(r => r.Year == year && r.Month == month && r.Employee.Id == employeeId);
            var operations = _operationRepository.Items;
            var data = from employee in dataEmployee
                    join operation in operations on employee.Operation.Id equals operation.Id
                    select new { employee.CountExecutedOperation, operation };

            return data.Sum(i => i.CountExecutedOperation * new OperationModel(i.operation).Price);
        }
    }
}
