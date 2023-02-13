using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class CoefficientUserDialogService : IUserDialog<CoefficientModel>
    {
        public bool ConfirmOperation(string info, string caption)
        {
            return MessageBox.Show(info,
                                  caption,
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Warning,
                                  MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }

        public bool Edit(CoefficientModel model)
        {
            var coeffVM = new CoefficientEditorViewModel(model);

            var coeffV = new CoefficientEditorWindow()
            {
                DataContext = coeffVM,
            };

            Func<bool> errorMessage = new Func<bool>(() =>
            {
                MessageBox.Show("Неправильный формат коэффициента. Необходимо ввести одно число с дробной частью через точку",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error,
                                MessageBoxResult.Cancel);
                return false;
            });

            if (coeffV.ShowDialog() != true) return false;
            var regex = new Regex(@"\b\d+\,\d{0,2}\b");
            var coeffValueString = coeffVM.CoefficientValue.ToString();
            if (int.TryParse(coeffValueString, out var num))
            {
                coeffVM.CoefficientValue = num;
            }
            else
            {
                var matches = regex.Matches(coeffValueString);
                if (matches.Count != 1)
                {
                    return errorMessage();
                }
                else
                {
                    if (!String.IsNullOrEmpty(coeffValueString.Replace(matches.First().ToString(), ""))) return errorMessage();
                    model.CoefficientValue = coeffVM.CoefficientValue;
                }
            }
            
            model.Name = coeffVM.Name;
            return true;
        }
    }
}
