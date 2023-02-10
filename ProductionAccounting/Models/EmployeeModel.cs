using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Models
{
    public class EmployeeModel : Model
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public EmployeeModel()
        {
        }

        public EmployeeModel(Employee orm)
        {
            Name = orm.Name;
            Surname = orm.Surname;
            Patronymic = orm.Patronymic;
            Id = orm.Id;
        }

        public Employee MapToOrm()
        {
            Employee emp = new Employee();
            emp.Id = Id;
            emp.Name = Name;
            emp.Surname = Surname;
            emp.Patronymic = Patronymic;
            return emp;
        }
    }
}
