using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionAccounting.Services
{
    public class AddingJobDataService : IAddingJobDataService
    {
        private readonly IRepository<ExecutedOperation> _repository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Operation> _operationRepository;
        public AddingJobDataService(IRepository<ExecutedOperation> executedOperationRepository, IRepository<Employee> employeeRepository, IRepository<Operation> operationRepository)
        {
            _repository = executedOperationRepository;
            _employeeRepository = employeeRepository;
            _operationRepository = operationRepository;
        }

        public int? GetNumberExecutedOperations(int employeeId, int operationId, int month, int year)
        {
            var items = _repository.Items.Where(i => i.Operation.Id == operationId
                                        && i.Employee.Id == employeeId
                                        && i.Month == month
                                        && i.Year == year);
            if (items.Count() > 1) throw new Exception();

            return items.FirstOrDefault()?.CountExecutedOperation ?? null;
        }

        public async Task InsertData(int employeeId, int operationId, int month, int year, int numberOfOperations)
        {
            var items = _repository.Items.Where(i => i.Operation.Id == operationId
                                        && i.Employee.Id == employeeId
                                        && i.Month == month
                                        && i.Year == year);
            if (items.Count() > 1) throw new Exception();
            var item = items.FirstOrDefault();
            if (item == null)
            {
                var newItem = new ExecutedOperation()
                {
                    Year = year,
                    Month = month,
                    Employee = _employeeRepository.GetById(employeeId),
                    Operation = _operationRepository.GetById(operationId),
                    CountExecutedOperation = numberOfOperations
                };
                _repository.Add(newItem);
            }
            else
            {
                item.CountExecutedOperation = numberOfOperations;
            }
            await _repository.SaveChangesAsync();
        }
    }
}
