using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;

namespace ProductionAccounting.ViewModels
{
    public class EmployeeEditorWindowViewModel : ViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                Set(ref _surname, value);
            }
        }

        private string _patronymic;
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                Set(ref _patronymic, value);
            }
        }

        public EmployeeEditorWindowViewModel(EmployeeModel employee)
        {
            Name = employee.Name;
            Surname = employee.Surname;
            Patronymic = employee.Patronymic;
        }

        public EmployeeEditorWindowViewModel()
        {

        }
    }
}
