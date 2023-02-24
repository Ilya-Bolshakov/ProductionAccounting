namespace ProductionAccounting.Services.Interfaces
{
    public interface ICalculateSalaryService
    {
        public decimal CalculateEmployeeSalary(int employeeId, int year, int month);
    }
}
