using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Models
{
    public class EmployeeModel : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }


        public EmployeeModel MapToDto(Employee orm)
        {
            return new EmployeeModel()
            {
                Id = orm.Id,
                Name = orm.Name,
                Surname = orm.Surname,
                Patronymic = orm.Patronymic,
            };
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
