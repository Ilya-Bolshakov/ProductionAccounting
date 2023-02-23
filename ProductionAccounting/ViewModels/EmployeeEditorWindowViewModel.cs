using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;
using ProductionAccounting.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProductionAccounting.ViewModels
{
    public class EmployeeEditorWindowViewModel : ValidationViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                ClearErrors(nameof(Name));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Name), "Имя должно быть заполнено");
                }
                else
                {
                    var regex = new Regex(@"^\s*[a-zA-Zа-яА-Я]+$");
                    value = value.Trim();
                    if (!regex.IsMatch(value))
                    {
                        AddError(nameof(Name), "Имя должно представлять из себя одно слово только из букв");
                    }
                    
                }
                Set(ref _name, value);
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                ClearErrors(nameof(Surname));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Surname), "Фамилия должна быть заполнена");
                }
                else
                {
                    var regex = new Regex(@"^\s*[a-zA-Zа-яА-Я]+$");
                    value = value.Trim();
                    if (!regex.IsMatch(value))
                    {
                        AddError(nameof(Surname), "Поле \"Фамилия\" должно представлять из себя одно слово только из букв");
                    }

                }
                Set(ref _surname, value);
            }
        }

        private string _patronymic;

        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                ClearErrors(nameof(Patronymic));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Patronymic), "Поле \"Отчество\" должно быть заполнено");
                }
                else
                {
                    var regex = new Regex(@"^\s*[a-zA-Zа-яА-Я]+$");
                    value = value.Trim();
                    if (!regex.IsMatch(value))
                    {
                        AddError(nameof(Patronymic), "Поле \"Отчество\" должно представлять из себя одно слово только из букв");
                    }

                }
                Set(ref _patronymic, value);
            }
        }

        public EmployeeEditorWindowViewModel(EmployeeModel employee)
        {
            Name = employee.Name;
            Surname = employee.Surname;
            Patronymic = employee.Patronymic;
        }
    }
}
