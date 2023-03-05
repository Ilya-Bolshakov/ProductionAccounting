using System.Threading.Tasks;

namespace ProductionAccounting.Services.Interfaces
{
    public interface ICalculateSalaryService
    {
        public decimal CalculateEmployeeSalary(int employeeId, int year, int month);
        public Task<decimal> CalculateEmployeeSalaryAsync(int employeeId, int year, int month);
    }
}
