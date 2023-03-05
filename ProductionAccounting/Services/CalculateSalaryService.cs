using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionAccounting.Services
{
    public class CalculateSalaryService : ICalculateSalaryService
    {
        IRepository<ExecutedOperation> _repository;
        IRepository<Operation> _operationRepository;
        IShowExceptionDialogService _showExceptionDialog;

        public CalculateSalaryService(IRepository<ExecutedOperation> repository, IRepository<Operation> operationRepository, IShowExceptionDialogService showExceptionDialogService)
        {
            _repository = repository;
            _operationRepository = operationRepository;
            _showExceptionDialog = showExceptionDialogService;
        }

        public decimal CalculateEmployeeSalary(int employeeId, int year, int month)
        {
            try
            {
                var dataEmployee = _repository.Items.Where(r => r.Year == year && r.Month == month && r.Employee.Id == employeeId);
                var operations = _operationRepository.Items;
                var data = from employee in dataEmployee
                           join operation in operations on employee.Operation.Id equals operation.Id
                           select new { employee.CountExecutedOperation, operation };
                var da = data.Select(d => new OperationModel(d.operation).Price * d.CountExecutedOperation).ToList();

                return da.Sum();
            }
            catch (System.Exception ex)
            {

                _showExceptionDialog.ShowDialog("Ошибка при подсчете зарплаты. Попробуйте еще раз.\nПоказать ошибку для разработчика?", ex.Message);
                return 0;
            }
        }

        public Task<decimal> CalculateEmployeeSalaryAsync(int employeeId, int year, int month)
        {
            return Task.Run(() =>
            {
                try
                {
                    var dataEmployee = _repository.Items.Where(r => r.Year == year && r.Month == month && r.Employee.Id == employeeId);
                    var operations = _operationRepository.Items;
                    var data = from employee in dataEmployee
                               join operation in operations on employee.Operation.Id equals operation.Id
                               select new { employee.CountExecutedOperation, operation };
                    var da = data.Select(d => new OperationModel(d.operation).Price * d.CountExecutedOperation).ToList();

                    return da.Sum();
                }
                catch (System.Exception ex)
                {

                    _showExceptionDialog.ShowDialog("Ошибка при подсчете зарплаты. Попробуйте еще раз.\nПоказать ошибку для разработчика?", ex.Message);
                    return 0;
                }
            });

        }
    }
}
