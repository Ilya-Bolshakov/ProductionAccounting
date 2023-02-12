using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Models
{
    public class EmployeeModel : Model
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        private string _surname;

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }
        private string _patronymic;

        public string Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = value; OnPropertyChanged(nameof(Patronymic)); }
        }


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

        public override object Clone()
        {
            EmployeeModel emp = new EmployeeModel();
            emp.Id = Id;
            emp.Name = Name;
            emp.Surname = Surname;
            emp.Patronymic = Patronymic;
            return emp;
        }
    }
}
